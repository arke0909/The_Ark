using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.Core.Manager
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel sceneChannel;

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
            SceneManager.LoadScene(evt.sceneName);
        }
    }
}