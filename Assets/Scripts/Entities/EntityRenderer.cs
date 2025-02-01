using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        protected SpriteRenderer _renderer;
        protected Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _renderer = GetComponent<SpriteRenderer>();
        }

        
    }
}