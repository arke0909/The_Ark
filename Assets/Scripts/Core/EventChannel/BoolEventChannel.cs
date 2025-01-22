using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Core.EventChannel
{
    [CreateAssetMenu(fileName = "BoolEvent", menuName = "SO/EventChannel/BoolEvent")]
    public class BoolEventChannel : EventChannel<bool> { }
}