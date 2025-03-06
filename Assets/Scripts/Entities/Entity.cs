using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
namespace Assets.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;

        private Dictionary<Type, IEntityComponent> _entityComponents = new Dictionary<Type, IEntityComponent>();

        public bool IsDead { get; private set; } = false;

        #region Init Section
        protected virtual void Awake()
        {
            SetEntityComponentsAndInitialize();
            AfterInitialize();

            turnChangeChannel.AddListener<TurnChangeEvent>(HandleTurnChange);
        }


        protected virtual void OnDestroy()
        {
            turnChangeChannel.RemoveListener<TurnChangeEvent>(HandleTurnChange);
        }

        private void SetEntityComponentsAndInitialize()
        {
            GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _entityComponents.Add(type, component);
            });
        }

        private void AfterInitialize()
        {
            _entityComponents.Values.OfType<IAfterInit>().ToList().ForEach(afterInit => afterInit.AfterInit());
        }
        #endregion

        public T GetCompo<T>() where T : class
        {
            Type type = typeof(T);

            if (_entityComponents.TryGetValue(type, out IEntityComponent compo))
            {
                return compo as T;
            }

            return default;
        }

        public void OnDead()
        {
            IsDead = true;
        }

        #region TurnChange Section
        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if (IsDead) return;

            switch (evt.nextTurn)
            {
                case "PLAYER":
                    PlayerTurn();
                    break;
                case "ENEMY":
                    EnemyTurn();
                    break;
                case "INPUT":
                    InputTurn();
                    break;
                case "DAMAGECALC":
                    DamageCalcTurn();
                    break;
            }
        }

        protected virtual void PlayerTurn()
        { }
        protected virtual void EnemyTurn()
        { }
        protected virtual void InputTurn()
        { }
        protected virtual void DamageCalcTurn()
        { }
        #endregion
    }
}