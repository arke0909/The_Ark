using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;

        #region EventChannel Section
        [Header("Event Channel")]
        [SerializeField] private ArrowTypeEventChannel arrowCheckEvent;
        [SerializeField] private BoolEventChannel changeIsCheckEvent;
        [SerializeField] private GameEventChannel attackChannel;
        #endregion

        private ArrowSetter _setterCompo;

        private ArrowType _currentArrow;
        private int _currentRepeatCnt = 0;

        private bool _isCheckTime;
        private float _currentTime;

        private int _idx;

        private void Awake()
        {
            _setterCompo = GetComponent<ArrowSetter>();

            arrowCheckEvent.ValueEvent += HandleArrowCheck;
            changeIsCheckEvent.ValueEvent += HandleChangeIsCheck;
        }

        private void OnDestroy()
        {
            arrowCheckEvent.ValueEvent -= HandleArrowCheck;
            changeIsCheckEvent.ValueEvent -= HandleChangeIsCheck;
        }

        private void Update()
        {
            if(_isCheckTime)
            {
                _currentTime += Time.deltaTime;
            }  
        }

        private void IndexInit()
        {
            if(_isCheckTime == false)
                _isCheckTime = true;
        }

        private void HandleArrowCheck(ArrowType type)
        {
            bool isRight = _currentArrow == type;

            if (isRight)
            {
                _idx++;

                if (_idx == _setterCompo.Arrows.Count)
                {
                    EndCheck();
                    return;
                }
            }
            else
            {
                _idx = 0;
            }

            _currentArrow = _setterCompo.Arrows[_idx];
        }

        private void HandleChangeIsCheck(bool value)
        {
            _isCheckTime = value;
        }

        private void EndCheck()
        {
            _currentRepeatCnt++;

            if (_currentRepeatCnt < totalRepeatCnt)
            {
                int size = _setterCompo.GetSize();
                _setterCompo.HandleSetArrows(size);
                _idx = 0;
                _currentArrow = _setterCompo.Arrows[_idx];
            }
            else
            {
                _isCheckTime = false;

                ApplyDamage(_currentTime);
                _currentRepeatCnt = 0;
                _currentTime = 0;
            }
        }

        private void ApplyDamage(float damge)
        {
            AttackEvent evt = CombatEvents.AttackEvent;
            evt.damage = damge;

            attackChannel.RaiseEvent(evt);
        }
    }
}