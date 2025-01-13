using UnityEngine;

public class Arrow
{
    private ArrowType _arrowType;

    public ArrowType ArrowType => _arrowType;

    public Arrow(ArrowType arrowType)
    {
        _arrowType = arrowType;
    }

    public bool Check(ArrowType arrowType) => _arrowType == arrowType;
}
