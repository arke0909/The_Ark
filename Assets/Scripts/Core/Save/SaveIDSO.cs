using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Save
{
    [CreateAssetMenu(fileName = "SaveIdSO", menuName = "SO/System/SaveId")]
    public class SaveIDSO : MonoBehaviour
    {
        public int saveID;
        public string saveName;
    }
}