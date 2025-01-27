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

        protected Dictionary<Type, IEntityComponent> _entityComponets = new Dictionary<Type, IEntityComponent>();

        #region Init Section
        protected virtual void Awake()
        {
            SetEntityCompoentsAndInitialize();
        }

        protected virtual void OnDestroy()
        {
        }

        private void SetEntityCompoentsAndInitialize()
        {
            GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _entityComponets.Add(type, component);
            });
        }
        #endregion

        public T GetCompo<T>() where T : class
        {
            Type type = typeof(T);

            if(_entityComponets.TryGetValue(type, out IEntityComponent compo))
            {
                return compo as T;
            }

            return default;
        }

        protected void TurnChangeCalling(bool isPlayerTurn)
        {
            TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
            evt.isPlayerTurn = isPlayerTurn;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}