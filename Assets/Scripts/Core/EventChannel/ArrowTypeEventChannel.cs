using Assets.Scripts.Core.EventChannel;
using UnityEngine;

namespace Scripts.Core.EventChannel
{
    [CreateAssetMenu(fileName = "ArrowTypeEvent", menuName = "SO/EventChannel/ArrowTypeEvent")]
    public class ArrowTypeEventChannel : EventChannel<ArrowType> { }
}