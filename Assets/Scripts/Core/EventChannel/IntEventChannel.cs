using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Core.EventChannel
{
    [CreateAssetMenu(fileName = "IntEvent", menuName = "SO/EventChannel/IntEvent")]
    public class IntEventChannel : EventChannel<int> { }
}