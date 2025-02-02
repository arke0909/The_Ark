using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Pools
{
    public class Pool
    {
        private Stack<IPoolable> _pool;
        private Transform _parent;
        private IPoolable _poolable;
        private GameObject _prefab;

        internal Pool(IPoolable poolalbe, Transform parent, int count)
        {
            _pool = new Stack<IPoolable>();
            _parent = parent;
            _poolable = poolalbe;
            _prefab = _poolable.PoolObject;

            for(int i = 0; i < count; i++)
            {
                GameObject gameObject = GameObject.Instantiate(_prefab, parent);
                gameObject.SetActive(false);
                gameObject.name = _poolable.PoolName;

                IPoolable item = gameObject.GetComponent<IPoolable>();
                
                _pool.Push(item);
            }
        }

        public IPoolable Pop()
        {
            IPoolable item = null;

            if (_pool.Count == 0)
            {
                GameObject gameObj = GameObject.Instantiate(_prefab, _parent);
                gameObj.name = _poolable.PoolName;
                item = gameObj.GetComponent<IPoolable>();
            }
            else
            {
                item = _pool.Pop();
                item.PoolObject.SetActive(true);
            }
            // 조건문을 거친 item을 리턴한다.
            return item;
        }

        public void Push(IPoolable item)
        {
            // 매개변수로 받아온 풀링한 오브젝트를 비활성화한다.
            item.PoolObject.SetActive(false);
            // 스택 클래스의 Push를 통해 풀에 집어넣는다.
            _pool.Push(item);
        }

    }
}
