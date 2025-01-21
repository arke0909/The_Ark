using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entity;
using Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Players
{
    public class Player : Entity
    {
        [field : SerializeField] public InputReader  InputCompo {  get; private set; }

        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private GameEventChannel turnChangeChannel;

        private Dictionary<Type, IPlayerComponent> _components;


        #region Init Section
        private void Awake()
        {
            _components = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

            InputCompo.ArrowEvent += CheckArrow;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleInputChange);
        }


        private void OnDestroy()
        {
            InputCompo.ArrowEvent -= CheckArrow;
        }
        private void SetPlayerCompoentsAndInitialize()
        {
            GetComponentsInChildren<IPlayerComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _components.Add(type, component);
            });
        }
        #endregion

        private void HandleInputChange(TurnChangeEvent evt)
        {
            InputCompo.TurnChange(evt.isPlayerTurn);
        }

        private void CheckArrow(ArrowType type)
        {
            arrowTypeEvent.RaiseEvent(type);
        }
    }
}
