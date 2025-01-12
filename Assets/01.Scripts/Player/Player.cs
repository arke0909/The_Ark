using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader  InputCompo {  get; private set; }

    [SerializeField] private ArrowTypeEventChannel ArrowTypeEvent;

    private void Awake()
    {
        InputCompo.ArrowEvent += CheckArrow;
    }

    private void CheckArrow(ArrowType type)
    {
        ArrowTypeEvent.RaiseEvent(type);
    }
}
