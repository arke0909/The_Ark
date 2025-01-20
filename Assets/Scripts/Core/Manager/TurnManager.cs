using Scripts.Core.EventChannel;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private GameEventChannel turnChangeChannel;

        [SerializeField] private List<ArrowType> _arrows;

        private ArrowType _currentArrow;

        private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

        private int _currentRepeatCnt = 0;

        [SerializeField] private int _idx;

        private int CurrentArrowIndex
        {
            get => _idx;

            set
            {
                if (_arrows.Count <= value)
                    value = 0;

                _idx = value;
            }
        }

        private int _size;

        public int totalRepeatCnt = 1;

        private void Awake()
        {
            _arrows = new List<ArrowType>();
            arrowTypeEvent.ValueEvent += ArrowCheck;

            SetArrows(8);
        }

        public void SetArrows(int size)
        {
            _arrows.Clear();
            CurrentArrowIndex = 0;
            //_size = size;

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
                if (CurrentArrowIndex == _arrows.Count - 1)
                {
                    End();
                    return;
                }
                else
                    CurrentArrowIndex++;
            }
            else
            {

                CurrentArrowIndex = 0;
            }


            _currentArrow = _arrows[CurrentArrowIndex];
        }

        [SerializeField] private Vector2 size;
        [SerializeField] private float _duration;

        private void End()
        {
            _currentRepeatCnt++;

            if (_currentRepeatCnt < totalRepeatCnt)
            {
                SetArrows(_size);
            }
            else
            {
                TurnChangeToEnemy(false, size, _duration);
            }
        }

        private void TurnChangeToEnemy(bool isPlayerTurn, Vector2 size, float duration)
        {
            InputChangeEvent changeEvt = TurnEvents.InputChangeEvent;
            changeEvt.isPlayerTurn = isPlayerTurn;

            ChangeAreaSizeEvent sizeEvt = TurnEvents.ChangeAreaSizeEvent;
            sizeEvt.size = size;
            sizeEvt.duration = duration;

            turnChangeChannel.RaiseEvent(sizeEvt);
            turnChangeChannel.RaiseEvent(changeEvt);
        }
    }
}