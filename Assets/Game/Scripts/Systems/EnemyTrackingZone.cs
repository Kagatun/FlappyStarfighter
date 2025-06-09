using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Systems
{
    public class EnemyTrackingZone : MonoBehaviour
    {
        [SerializeField] private float _radius = 11f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _checkInterval = 0.2f;

        private WaitForSeconds _wait;
        private Coroutine _searchCoroutine;
        
        public event Action NoEnemiesDetected;

        private void Awake()
        {
            _wait = new WaitForSeconds(_checkInterval);
        }

        public void StartSearching()
        {
            if (_searchCoroutine != null)
                StopCoroutine(_searchCoroutine);
            
            _searchCoroutine = StartCoroutine(SearchRoutine());
        }
        
        private void CheckEnemies()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);

            if (hits.Length == 0)
                NoEnemiesDetected?.Invoke();
        }
        
        private IEnumerator SearchRoutine()
        {
            while (enabled)
            {
                CheckEnemies();
                
                yield return _wait;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}