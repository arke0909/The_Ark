using System;
using UnityEngine;

[Serializable]
public class Arrow
{
    [SerializeField]
    private ArrowType _arrowType;

    public ArrowType ArrowType => _arrowType;

    public Arrow(ArrowType arrowType)
    {
        _arrowType = arrowType;
    }

    public bool Check(ArrowType arrowType) => _arrowType == arrowType;
}
