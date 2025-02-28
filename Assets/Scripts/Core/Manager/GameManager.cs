using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Enemies;
using Assets.Scripts.Entities;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel uiChannel;
        [SerializeField] private EntityFinder playerFinder;
        [SerializeField] private EntityFinder enemyFinder;
        private Player _player;
        private Enemy _enemy;

        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _enemy = FindAnyObjectByType<Enemy>();
            playerFinder.SetEntity(_player);
            enemyFinder.SetEntity(_enemy);
        }

        private void Start()
        {
            FadeEvent evt = CoreEvents.FadeEvent;
            evt.isClear = false;
            evt.isFading = false;

            uiChannel.RaiseEvent(evt);
        }
    }
}