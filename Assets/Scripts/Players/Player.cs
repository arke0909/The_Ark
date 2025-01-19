using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Players
{
    public class Player : Entity
    {
        [field : SerializeField] public InputReader  InputCompo {  get; private set; }

        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private BoolEventChannel inputChangeChannel;

        private Dictionary<Type, IPlayerComponent> _components;

        private void Awake()
        {
            _components = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

            InputCompo.ArrowEvent += CheckArrow;
            inputChangeChannel.ValueEvent += TurnChange;
        }

        private void OnDestroy()
        {
            InputCompo.ArrowEvent -= CheckArrow;
            inputChangeChannel.ValueEvent -= TurnChange;
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

        private void CheckArrow(ArrowType type)
        {
            arrowTypeEvent.RaiseEvent(type);
        }

        private void TurnChange(bool isPlayerTurn)
        {
            InputCompo.TurnChange(isPlayerTurn);
        }
    }
}
