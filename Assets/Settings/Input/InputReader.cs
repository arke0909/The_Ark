using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Input;

public enum ArrowType
{
    UP, DOWN, LEFT, RIGHT
}

[CreateAssetMenu(fileName = "InputReader", menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerTurnActions, IEnemyTurnActions, IBattleActions, IUIActions
{
    public event Action<ArrowType> BattleEvent;
    public event Action<(int x, int y)> PlayerTurnInputEvent;
    public event Action SelectEvent;
    public event Action EscapeEvent;

    public Vector2 InputVector { get; private set; }

    private Input _input;
    private bool _isBattle;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new Input();

            _input.PlayerTurn.SetCallbacks(this);
            _input.Battle.SetCallbacks(this);
            _input.EnemyTurn.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
        }

        _input.PlayerTurn.Enable();
        _input.UI.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public void TurnChange(bool isPlayerTurn)
    {
        _isBattle = false;

        _input.Disable();
        _input.UI.Enable();

        if (isPlayerTurn)
            _input.PlayerTurn.Enable();
        else
            _input.EnemyTurn.Enable();
    }

    public void Battle()
    {
        _input.Disable();

        _isBattle = true;
        _input.UI.Enable();
        _input.Battle.Enable();
    }

    private bool isChoice()
    {
        return _input.PlayerTurn.enabled;
    }


    #region Player Turn

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                PlayerTurnInputEvent?.Invoke(ValueTuple.Create(0, 1));
            else
                BattleEvent?.Invoke(ArrowType.UP);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                PlayerTurnInputEvent?.Invoke(ValueTuple.Create(0, -1));
            else
                BattleEvent?.Invoke(ArrowType.DOWN);
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                PlayerTurnInputEvent?.Invoke(ValueTuple.Create(-1, 0));
            else
                BattleEvent?.Invoke(ArrowType.LEFT);
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isChoice())
                PlayerTurnInputEvent?.Invoke(ValueTuple.Create(1, 0));
            else
                BattleEvent?.Invoke(ArrowType.RIGHT);
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectEvent?.Invoke();
    }

    #endregion

    #region Enemy Turn
    public void OnMove(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }


    #endregion

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
            EscapeEvent?.Invoke();
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            if(_isBattle)
            {
                _input.Battle.Enable();
            }
            else
            {
                _input.EnemyTurn.Enable();
                _input.PlayerTurn.Enable();
            }
        }
        else
        {
            _input.EnemyTurn.Disable();
            _input.PlayerTurn.Disable();
            _input.Battle.Disable();
        }
    }

    public void InputClear()
    {
        BattleEvent = null;
        PlayerTurnInputEvent = null;
        SelectEvent = null;
        EscapeEvent = null;
    }
}
