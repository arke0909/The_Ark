using System.Collections;
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

        public void InitBullet(Vector2 dir)
        {

        }
    }
}