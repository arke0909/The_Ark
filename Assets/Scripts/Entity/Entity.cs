using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected GameEventChannel turnChangeChannel;
    }
}