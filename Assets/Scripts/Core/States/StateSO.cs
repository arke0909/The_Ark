using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.States
{
    [CreateAssetMenu(fileName = "State", menuName = "SO/States/State")]
    public class StateSO : ScriptableObject
    {
        public string stateName;
        public string className;
    }
}