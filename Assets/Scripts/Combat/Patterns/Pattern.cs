using Assets.Scripts.Combat.Bullets;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        [SerializeField] protected List<Transform> firePosTrm = new List<Transform>();

        public Vector2 areaSize;
        public Bullet bullet;

        public abstract void UsePattern();
    }
}