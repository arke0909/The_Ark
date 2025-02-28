using Assets.Scripts.Core;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Retry : Act
    {
        [SerializeField] private SceneManagerSO sceneManagerSO;
        [SerializeField] private GameEventChannel sceneChannel;

        public override void ActEffect()
        {
            FadeEvent evt = CoreEvents.FadeEvent;
            evt.isFading = true;
            evt.isClear = false;
            evt.sceneName = sceneManagerSO.GetCurrentSceneName();

            sceneChannel.RaiseEvent(evt);
        }
    }
}