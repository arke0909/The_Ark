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

        public void FadeWithTurn(bool isPlayerTurn)
        {
            float alpha = isPlayerTurn ? 0.0f : 1.0f;
            Color color = _renderer.color;
            color.a = alpha;

            _renderer.color = color;
        }
    }
}