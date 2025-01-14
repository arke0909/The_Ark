using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowsCreater : MonoBehaviour
{
    [SerializeField] private ArrowTypeEventChannel ArrowTypeEvent;

    private List<ArrowType> _arrows;

    private ArrowType _currentArrow;

    private int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

    //private int _currentRepeatCnt = 0;

    private int _idx;

    private int _currentArrowIndex
    {
        get
        {
            return _idx;
        }

        set
        {
            if (_arrows.Count <= value)
                value = 0;

            _idx = value;
        }
    }

    private int _size;

    //public int TotalRepeatCnt { get; set; }

    private void Awake()
    {
        _arrows = new List<ArrowType>();
        ArrowTypeEvent.ValueEvent += ArrowCheck;

        SetArrows(8);
    }

    public void SetArrows(int size)
    {
        _arrows.Clear();
        _currentArrowIndex = 0;
        //_size = size;

        for (int i = 0; i < size; i++)
        {
            ArrowType arrow = (ArrowType)Random.Range(0, _arrowTypeCnt);
            _arrows.Add(arrow);
        }

        _currentArrow = _arrows[_currentArrowIndex];
    }

    private void ArrowCheck(ArrowType type)
    {
        bool isRight = _currentArrow == type;

        if (isRight)
        {
            if (_currentArrowIndex == _arrows.Count - 1)
            {
                End();
                return;
            }
            else
                _currentArrowIndex++;
        }
        else
        {

            _currentArrowIndex = 0;
        }


        _currentArrow = _arrows[_currentArrowIndex];
    }

    private void End()
    {
        /*if (_currentRepeatCnt < TotalRepeatCnt)
        {
            _currentRepeatCnt++;
            SetArrows(_size);
        }
        else
        {
        }*/
    }
}
