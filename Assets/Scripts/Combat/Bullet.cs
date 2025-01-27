using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rigidCompo;

        private void Awake()
        {
            rigidCompo = GetComponent<Rigidbody2D>();
        }

        public void InitBullet(Vector2 dir, float speed)
        {
            rigidCompo.linearVelocity = dir.normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player hit");
                Destroy(gameObject);
            }
        }
    }
}