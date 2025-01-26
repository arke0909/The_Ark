using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Player : Entity
    {
        [SerializeField] private ArrowTypeEventChannel arrowCheckChannel;
        [field: SerializeField] public InputReader InputCompo { get; private set; }

        

        private Dictionary<Type, IPlayerComponent> _components;


        #region Init Section
        private void Awake()
        {
            _components = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

            InputCompo.BattleEvent += CheckArrow;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleInputChange);
        }
        private void OnDestroy()
        {
            InputCompo.BattleEvent -= CheckArrow;
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleInputChange);
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
            arrowCheckChannel.RaiseEvent(type);
        }
    }
}
