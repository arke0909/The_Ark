using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Players
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private float moveSpeed = 5f;

        private Player _player;
        private Rigidbody2D _rbCompo;

        public bool canManualMove = true;

        public void Initialize(Player player)
        {
            _player = player;
            _rbCompo = _player.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!canManualMove) return;

            Movement(_player.InputCompo.InputVector);
        }

        private void Movement(Vector2 inputVector)
        {
            _rbCompo.linearVelocity = inputVector * moveSpeed;
        }
    }
}
