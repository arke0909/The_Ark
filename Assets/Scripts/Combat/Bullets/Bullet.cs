using UnityEngine;

namespace Assets.Scripts.Combat.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;

        private float _currentLifeTime = 0;

        private Rigidbody2D rigidCompo;

        private void Awake()
        {
            rigidCompo = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _currentLifeTime += Time.deltaTime;
            
            if(_currentLifeTime >= lifeTime)
                Destroy(gameObject);
        }

        public void InitBullet(Vector2 dir)
        {
            transform.right = dir.normalized;
            rigidCompo.linearVelocity = transform.right * speed;
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