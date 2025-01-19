using Assets.Scripts.Entity;
using Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    // ��ũ��Ʈ ���� ���� Ȯ��
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EntityFinder playerFinder;
        private Player _player;

        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            playerFinder.SetEntity(_player);
        }
    }
}