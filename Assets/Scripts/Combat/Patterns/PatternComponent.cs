using Assets.Scripts.Entities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Combat.Skills
{
    public class PatternComponent : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
    }
}