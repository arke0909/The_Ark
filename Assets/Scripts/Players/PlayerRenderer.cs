using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerRenderer : EntityRenderer, IPlayerComponent
    {
        public void Initialize(Player player)
        {
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