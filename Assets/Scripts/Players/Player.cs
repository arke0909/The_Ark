using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities;
using Scripts.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public InputReader InputCompo { get; private set; }

        [SerializeField] private ArrowTypeEventChannel arrowCheckChannel;
        [SerializeField] private GameEventChannel playerDeadChannel;

        private Dictionary<Type, IPlayerComponent> _playerComponents;

        private Vector2 _originPos;

        #region Init Section
        protected override void Awake()
        {
            base.Awake();

            _originPos = transform.position;

            _playerComponents = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

            playerDeadChannel.AddListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
            InputCompo.BattleEvent += CheckArrow;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            InputCompo.BattleEvent -= CheckArrow;
        }
        private void SetPlayerCompoentsAndInitialize()
        {
            GetComponentsInChildren<IPlayerComponent>().ToList().ForEach(component =>
            {
                Type type = component.GetType();
                component.Initialize(this);
                _playerComponents.Add(type, component);
            });
        }
        #endregion

        public T GetPlayerCompo<T>() where T : class
        {
            Type type = typeof(T);

            if(_playerComponents.TryGetValue(type, out IPlayerComponent compo))
            {
                return compo as T;
            }

            return default;
        }

        private void CheckArrow(ArrowType type)
        {
            arrowCheckChannel.RaiseEvent(type);
        }

        private void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            InputCompo.TurnChange(true);
            GetPlayerCompo<PlayerMovement>().SetCanMove(true);
        }

        protected override void PlayerTurn()
        {
            InputCompo.TurnChange(true);
            GetPlayerCompo<PlayerRenderer>().FadeWithTurn(true);
        }

        protected override void EnemyTurn()
        {
            transform.position = _originPos;
            InputCompo.TurnChange(false);
            GetPlayerCompo<PlayerRenderer>().FadeWithTurn(false);
            GetPlayerCompo<PlayerMovement>().SetCanMove(true);
        }

        protected override void DamageCalcTurn()
        {
            GetPlayerCompo<PlayerMovement>().SetCanMove(false);
            InputCompo.TurnChange(true);
        }

        protected override void InputTurn()
        {
            InputCompo.Battle();
        }
    }
}
