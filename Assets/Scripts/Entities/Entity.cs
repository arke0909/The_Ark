using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
namespace Assets.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;

        protected Dictionary<Type, IEntityComponent> _entityComponents = new Dictionary<Type, IEntityComponent>();

        public bool IsDead { get; private set; } = false;

        #region Init Section
        protected virtual void Awake()
        {
            SetEntityCompoentsAndInitialize();
            AfterInitalize();

            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
            turnChangeChannel.AddListner<PriorityTurnChangeEvent>(HandlePriorityTurnChange);
        }


        protected virtual void OnDestroy()
        {
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
            turnChangeChannel.RemoveListner<PriorityTurnChangeEvent>(HandlePriorityTurnChange);

            GetCompo<EntityStatComponent>().ClearAllStatModifier();
        }

        private void SetEntityCompoentsAndInitialize()
        {
            GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _entityComponents.Add(type, component);
            });
        }

        private void AfterInitalize()
        {
            _entityComponents.Values.OfType<IAfterInit>().ToList().ForEach(afterInit => afterInit.AfterInit());
        }
        #endregion

        public T GetCompo<T>() where T : class
        {
            Type type = typeof(T);

            if(_entityComponents.TryGetValue(type, out IEntityComponent compo))
            {
                return compo as T;
            }

            return default;
        }

        #region TurnChange Section
        protected void HandleTurnChange(TurnChangeEvent evt)
        {
            switch(evt.nextTurn)
            {
                case "PLAYER":
                    PlayerTurn();
                    break;
                case "ENEMY":
                    EnemyTurn();
                    break;
                case "HEAL":
                    HealTurn();
                    break;
                case "BUFF":
                    BuffTurn();
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
        protected virtual void HealTurn()
        { }
        protected virtual void BuffTurn()
        { }
        protected virtual void InputTurn()
        { }
        protected virtual void DamageCalcTurn()
        { }
        #endregion

        #region Priority TurnChange Section
        private void HandlePriorityTurnChange(PriorityTurnChangeEvent evt)
        {
            switch (evt.nextTurn)
            {
                case "PLAYER":
                    PriorityPlayerTurn();
                    break;
                case "ENEMY":
                    PriorityEnemyTurn();
                    break;
                case "HEAL":
                    PriorityHealTurn();
                    break;
                case "BUFF":
                    PriorityBuffTurn();
                    break;
                case "INPUT":
                    PriorityInputTurn();
                    break;
                case "DAMAGECALC":
                    PriorityDamageCalcTurn();
                    break;
            }
        }

        protected virtual void PriorityPlayerTurn()
        { }
        protected virtual void PriorityEnemyTurn()
        { }
        protected virtual void PriorityHealTurn()
        { }
        protected virtual void PriorityBuffTurn()
        { }
        protected virtual void PriorityInputTurn()
        { }
        protected virtual void PriorityDamageCalcTurn()
        { }

        #endregion
    }
}