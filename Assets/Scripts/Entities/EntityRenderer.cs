using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        private SpriteRenderer _renderer;
        private Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _renderer = GetComponent<SpriteRenderer>();
        }


    }
}