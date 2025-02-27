using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    [CreateAssetMenu(fileName = "SceneManagerSO", menuName = "SO/System/SceneManagerSO")]
    public class SceneManagerSO : ScriptableObject
    {
        public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;

        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}
