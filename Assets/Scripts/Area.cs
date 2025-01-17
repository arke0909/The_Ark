using Scripts.Player;
using UnityEngine;

namespace Assets.Scripts
{
    public class Area : MonoBehaviour
    {
        private Collider2D confiningBounds; // 경계가 될 Collider2D
        [SerializeField] private Player _player;

        private void Awake()
        {
            confiningBounds = GetComponent<Collider2D>();
            Debug.Assert(confiningBounds != null, "this gameObject has not Collider2D");
        }

        private void FixedUpdate()
        {
            if (confiningBounds == null) return;

            // 현재 위치가 경계 내에 있는지 확인
            Vector2 currentPosition = _player.transform.position;

            // 경계 안의 가장 가까운 점 계산
            Vector2 closestPoint = confiningBounds.ClosestPoint(currentPosition);

            // 오브젝트가 경계를 벗어났다면 위치를 경계로 이동
            if (currentPosition != closestPoint)
            {
                _player.transform.position = closestPoint;
            }
        }
    }
}