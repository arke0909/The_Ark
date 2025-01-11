using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Input;

public enum ArrowsType
{
    UP, DOWN, LEFT, RIGHT
}

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerTurnActions
{
    public Action<ArrowsType> ArrowEvent;

    private Input _input;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Input();

            _input.PlayerTurn.SetCallbacks(this);
        }

        _input.PlayerTurn.Enable();
    }

    private void OnDisable()
    {
        _input.PlayerTurn.Disable();
    }

    #region Player Turn

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            ArrowEvent?.Invoke(ArrowsType.UP);
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if(context.performed)
            ArrowEvent?.Invoke(ArrowsType.DOWN);
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            ArrowEvent?.Invoke(ArrowsType.LEFT);
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            ArrowEvent?.Invoke(ArrowsType.RIGHT);
    }

    #endregion
}
