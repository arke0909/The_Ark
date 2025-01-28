using UnityEngine;

namespace Assets.Scripts.Entities
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/Entities/Finder")]
    public class EntityFinder : ScriptableObject
    {
        public Entity entity;

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        } 
    }
}