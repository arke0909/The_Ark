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

        public Pool(IPoolable poolalbe, Transform parent, int count)
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

            return item;
        }

        public void Push(IPoolable item)
        {
            item.PoolObject.SetActive(false);
            _pool.Push(item);
        }

    }
}
