using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field : SerializeField] public InputReader  InputCompo {  get; private set; }

    [SerializeField] private ArrowTypeEventChannel ArrowTypeEvent;

    private Dictionary<Type, IPlayerComponent> _components;

    private void Awake()
    {
        _components = new Dictionary<Type, IPlayerComponent>();
        SetPlayerCompoentsAndInitialize();


        InputCompo.ArrowEvent += CheckArrow;
    }

    private void SetPlayerCompoentsAndInitialize()
    {
        GetComponentsInChildren<IPlayerComponent>().ToList().ForEach(component =>
        {
            Type type = component.GetType();
            component.Initialize(this);
            _components.Add(type, component);
        });
    }

    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            InputCompo.TurnChange(false);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            InputCompo.TurnChange(true);
        }
    }

    private void CheckArrow(ArrowType type)
    {
        ArrowTypeEvent.RaiseEvent(type);
    }
}
