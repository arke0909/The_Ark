using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Input;

public enum ArrowType
{
    UP, DOWN, LEFT, RIGHT
}

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerTurnActions, IEnemyTurnActions, IChoiceActions
{
    public event Action<ArrowType> ArrowEvent;
    public event Action ChoiceEvent;

    public Vector2 InputVector { get; private set; }

    private Input _input;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Input();

            _input.PlayerTurn.SetCallbacks(this);
            _input.EnemyTurn.SetCallbacks(this);
        }

        _input.PlayerTurn.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    public void TurnChange(bool isPlayerTurn)
    {
        _input.Disable();

        if (isPlayerTurn)
            _input.PlayerTurn.Enable();
        else
            _input.EnemyTurn.Enable();
    }

    private bool isChoice()
    {
        return _input.Choice.enabled;
    }

    #region Player Turn

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                ChoiceEvent?.Invoke();
            else
                ArrowEvent?.Invoke(ArrowType.UP);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (isChoice())
                ChoiceEvent?.Invoke();
            else
                ArrowEvent?.Invoke(ArrowType.DOWN);
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                ChoiceEvent?.Invoke();
            else
                ArrowEvent?.Invoke(ArrowType.LEFT);
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                ChoiceEvent?.Invoke();
            else
                ArrowEvent?.Invoke(ArrowType.RIGHT);
        }
    }

    #endregion

    #region Enemy Turn
    public void OnMove(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }
    #endregion
}
