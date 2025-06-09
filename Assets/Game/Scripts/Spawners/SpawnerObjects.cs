using UnityEngine;
using UnityEngine.Pool;

namespace Scripts.Spawner
{
    public abstract class SpawnerObjects<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField] private T _prefabs;

        private ObjectPool<T> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true);
        }

        protected void AddToPool(T obj) =>
            _pool.Release(obj);

        protected T Get() =>
            _pool.Get();

        protected virtual T CreateObject() =>
            Instantiate(_prefabs);

        protected virtual void OnGet(T enemy) =>
            enemy.gameObject.SetActive(true);

        protected virtual void OnRelease(T enemy) =>
            enemy.gameObject.SetActive(false);

        protected virtual void Destroy(T obj) =>
            Destroy(obj.gameObject);
    }
}