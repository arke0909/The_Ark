using UnityEngine;

namespace Assets.Scripts.Entity
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/Entity/Finder")]
    public class EntityFinder : ScriptableObject
    {
        public Entity entity;

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }
    }
}