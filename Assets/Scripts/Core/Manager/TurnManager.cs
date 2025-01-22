using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;

        private void Awake()
        {
            //turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange()
        {

        }
    }
}