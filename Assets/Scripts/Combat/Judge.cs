using Scripts.Core.EventChannel;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Judge : MonoBehaviour
{
    [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
    [SerializeField] private IntEventChannel setRepeatCntEvent;
    [SerializeField] private List<ArrowType> _arrows;

    private int totalRepeatCnt = 1;

    private ArrowType _currentArrow;
    private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;
    private int _currentRepeatCnt = 0;

    private int _size;
    private int _idx;

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

    private void Awake()
    {
        _arrows = new List<ArrowType>();

        arrowTypeEvent.ValueEvent += ArrowCheck;
        setRepeatCntEvent.ValueEvent += SetArrows;
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

    private void End()
    {
        _currentRepeatCnt++;

        if (_currentRepeatCnt < totalRepeatCnt)
        {
            SetArrows(_size);
        }
        else
        {
            //TurnChangeToEnemy();
        }
    }
}
