using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Players
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private StatSO moveSpeed;
        private float _moveSpeed;

        private Player _player;
        private Rigidbody2D _rbCompo;

        private bool _canManualMove = true;

        public void Initialize(Player player)
        {
            _player = player;
            _rbCompo = _player.GetComponent<Rigidbody2D>();

            _moveSpeed = _player.GetCompo<EntityStatComponent>().GetStat(moveSpeed).BaseValue;
        }

        private void FixedUpdate()
        {
            if (!_canManualMove) return;

            Movement(_player.InputCompo.InputVector);
        }

        private void Movement(Vector2 inputVector)
        {
            _rbCompo.linearVelocity = inputVector * _moveSpeed;
        }

        public void SetCanMove(bool value) => _canManualMove = value;
    }
}
