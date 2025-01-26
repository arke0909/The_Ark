using Assets.Scripts.Entities;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    // ��ũ��Ʈ ���� ���� Ȯ��
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private EntityFinder enemyFinder;
        private Player _player;
        private Player _enemy;

        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            playerFinder.SetEntity(_player);
            enemyFinder.SetEntity(_enemy);
        }
    }
}