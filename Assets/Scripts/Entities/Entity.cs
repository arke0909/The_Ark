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

        protected Dictionary<Type, IEntityComponent> _entityComponents = new Dictionary<Type, IEntityComponent>();

        #region Init Section
        protected virtual void Awake()
        {
            SetEntityCompoentsAndInitialize();
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        protected virtual void OnDestroy()
        {
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
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

        protected void HandleTurnChange(TurnChangeEvent evt)
        {
            switch(evt.turnState)
            {
                case "PLAYER":
                    PlayerTurn();
                    break;
                case "ENEMY":
                    EnemyTurn();
                    break;
                case "ITEM":
                    ItemTurn();
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
        protected virtual void ItemTurn()
        { }
        protected virtual void InputTurn()
        { }
        protected virtual void DamageCalcTurn()
        { }


    }
}