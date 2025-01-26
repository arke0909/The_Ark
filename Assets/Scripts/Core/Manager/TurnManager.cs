using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.InGameData;
using UnityEngine;

namespace Scripts.Core.Manager
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private Vector2Data originSize;

        private void Awake()
        {
            //turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange()
        {

        }
    }
}