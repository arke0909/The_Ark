using Assets.Scripts.Core.EventChannel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private BoolEventChannel fadeChannel;

    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            fadeChannel.RaiseEvent(true);
        }
    }
}
