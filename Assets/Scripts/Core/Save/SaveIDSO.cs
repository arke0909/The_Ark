using UnityEngine;

namespace Assets.Scripts.Core.Save
{
    [CreateAssetMenu(fileName = "SaveIdSO", menuName = "SO/System/SaveId")] 
    public class SaveIDSO : ScriptableObject
    {
        public int saveID;
        public string saveName;
    }
}