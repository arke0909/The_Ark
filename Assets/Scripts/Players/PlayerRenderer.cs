using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerRenderer : EntityRenderer, IPlayerComponent
    {
        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
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