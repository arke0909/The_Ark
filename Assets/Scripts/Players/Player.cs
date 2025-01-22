using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using Scripts.Core.EventChannel;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public InputReader InputCompo { get; private set; }

        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private GameEventChannel attackChannel;

        private Dictionary<Type, IPlayerComponent> _components;


        #region Init Section
        private void Awake()
        {
            _components = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

            InputCompo.BattleEvent += CheckArrow;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleInputChange);
            attackChannel.AddListner<AttackEvent>(HandleAttackEvent);
        }
        private void OnDestroy()
        {
            InputCompo.BattleEvent -= CheckArrow;
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleInputChange);
            attackChannel.RemoveListner<AttackEvent>(HandleAttackEvent);
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

        private void HandleAttackEvent(AttackEvent evt)
        {
            evt.damage = 5f;
        }

        private void CheckArrow(ArrowType type)
        {
            arrowTypeEvent.RaiseEvent(type);
        }
    }
}
