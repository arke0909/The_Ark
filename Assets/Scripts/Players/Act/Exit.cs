using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Exit : Act
    {
        [SerializeField] private GameEventChannel sceneChannel;

        public override void ActEffect()
        {
            FadeEvent evt = CoreEvents.FadeEvent;
            evt.isFading = true;
            evt.sceneName = "MainTitle";

            sceneChannel.RaiseEvent(evt);
        }
    }
}