using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;


namespace Assets.Scripts.Core.Manager
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel sceneChannel;
        [SerializeField] private SceneManagerSO sceneManagerSO;

        private void Awake()
        {
            sceneChannel.AddListener<SceneEvent>(HandleSceneEvent);
        }

        private void OnDestroy()
        {
            sceneChannel.RemoveListener<SceneEvent>(HandleSceneEvent);
        }

        private void HandleSceneEvent(SceneEvent evt)
        {
            Time.timeScale = 1;
            sceneManagerSO.LoadScene(evt.sceneName);
            Debug.Log(1);
        }
    }
}