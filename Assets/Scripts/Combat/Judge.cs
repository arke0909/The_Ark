using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Scripts.Core.EventChannel;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Combat
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;

        #region EventChannel Section
        [Header("Event Channel")]
        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private IntEventChannel setArrowEvent;
        #endregion

        [SerializeField] private List<ArrowType> _arrows;

        private ArrowType _currentArrow;
        private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;
        private int _currentRepeatCnt = 0;

        private bool _isCheckTime;
        private float _currentTime;

        private int _idx;

        private int CurrentArrowIndex
        {
            get => _idx;

            set
            {
                if (_arrows.Count < value)
                    value = 0;

                _idx = value;
            }
        }

        private int _size;
        private void Awake()
        {
            _arrows = new List<ArrowType>();

            arrowTypeEvent.ValueEvent += ArrowCheck;
            setArrowEvent.ValueEvent += SetArrows;
        }

        private void OnDestroy()
        {
            arrowTypeEvent.ValueEvent -= ArrowCheck;
            setArrowEvent.ValueEvent -= SetArrows;
        }

        public void SetArrows(int size)
        {
            if(_isCheckTime == false)
                _isCheckTime = true;

            _arrows.Clear();
            CurrentArrowIndex = 0;
            _size = size;

            for (int i = 0; i < size; i++)
            {
                ArrowType arrow = (ArrowType)Random.Range(0, _arrowTypeCnt);
                _arrows.Add(arrow);
            }

            _currentArrow = _arrows[CurrentArrowIndex];
        }

        private void ArrowCheck(ArrowType type)
        {
            bool isRight = _currentArrow == type;

            if (isRight)
            {
                CurrentArrowIndex++;

                if (CurrentArrowIndex == _arrows.Count)
                {
                    EndCheck();
                    return;
                }
            }
            else
            {
                CurrentArrowIndex = 0;
            }


            _currentArrow = _arrows[CurrentArrowIndex];
        }

        private void EndCheck()
        {
            _currentRepeatCnt++;

            if (_currentRepeatCnt < totalRepeatCnt)
            {
                SetArrows(_size);
            }
            else
            {
                _isCheckTime = false;

                ApplyDamage();
            }
        }

        private void ApplyDamage()
        {
            TurnChangeEvent evt = TurnEvents.TurnChangeEvent;
            evt.isPlayerTurn = false;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}