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

        

        private Dictionary<Type, IPlayerComponent> _playerComponents;


        #region Init Section
        protected override void Awake()
        {
            base.Awake();

            _playerComponents = new Dictionary<Type, IPlayerComponent>();
            SetPlayerCompoentsAndInitialize();

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

        protected override void HandleTurnChange(TurnChangeEvent evt)
        {
            //InputCompo.TurnChange(evt.isPlayerTurn);
        }
    }
}
