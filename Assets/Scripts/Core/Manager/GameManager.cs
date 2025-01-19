using Scripts.Players;
using UnityEngine;

// ��ũ��Ʈ ���� ����
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
