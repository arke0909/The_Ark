using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public abstract class Act : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;

        public abstract void OnClick();
    }
}