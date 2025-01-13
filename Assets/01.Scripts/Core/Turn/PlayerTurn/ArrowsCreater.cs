using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowsCreater : MonoBehaviour
{
    [SerializeField] private ArrowTypeEventChannel ArrowTypeEvent;

    [SerializeField] private List<Arrow> _arrows;
    private Arrow _currentArrow;

    private int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

    //private int _currentRepeatCnt = 0;

    [SerializeField] private int _idx;

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
        _arrows = new List<Arrow>();
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
            ArrowType arrowType = (ArrowType)Random.Range(0, _arrowTypeCnt);
            Arrow arrow = new Arrow(arrowType);
            _arrows.Add(arrow);
        }

        _currentArrow = _arrows[_currentArrowIndex];
    }

    private void ArrowCheck(ArrowType type)
    {
        if (_currentArrow.Check(type))
        {
            Debug.Log($"{_currentArrow.ArrowType}, {type} 같은 종류다.");
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

            Debug.Log($"{_currentArrow.ArrowType}, {type} 다른 종류다.");
            _currentArrowIndex = 0;
        }


        _currentArrow = _arrows[_currentArrowIndex];
    }

    private void End()
    {
        Debug.Log("끝");

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
