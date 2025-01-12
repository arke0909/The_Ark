using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Arrows : MonoBehaviour
{
    [SerializeField] private ArrowTypeEventChannel ArrowTypeEvent;
    [SerializeField] private int testSize = 5;

    private List<ArrowType> _arrows;
    private ArrowType _currentArrow;

    private int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

    private int _currentArrowIndex;

    public int CurrentArrowIndex
    {
        get { return _currentArrowIndex; }

        set
        {
            if (value < 0 || value > _arrows.Count)
                value = 0;

            _currentArrowIndex = value;
        }
    }


    private void Awake()
    {
        _arrows = new List<ArrowType>();
        ArrowTypeEvent.ValueEvent += ArrowCheck;

        SetArrows(testSize);
    }

    public void SetArrows(int size)
    {
        _arrows.Clear();
        _currentArrowIndex = 0;

        for (int i = 0; i < size; i++)
        {
            ArrowType arrow = (ArrowType)Random.Range(0, _arrowTypeCnt);
            _arrows.Add(arrow);
            Debug.Log(arrow);
        }

        _currentArrow = _arrows[_currentArrowIndex];
    }

    private void ArrowCheck(ArrowType type)
    {
        if (_currentArrow == type)
            _currentArrowIndex++;
        else
            _currentArrowIndex = 0;

            _currentArrow = _arrows[_currentArrowIndex];
    }
}
