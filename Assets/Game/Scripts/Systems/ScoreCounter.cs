using Scripts.PlayerUFO;
using Scripts.Shooting;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Systems
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreView  _scoreView;
        [SerializeField] private PlayerLaser _laser;
        
        public int CurrentScore {get; private set;}

        private void OnEnable()
        {
            _laser.Hited += OnAddCount;
        }

        private void OnDisable()
        {
            _laser.Hited -= OnAddCount;
        }

        public void Subscribe(Bullet bullet)
        {
            bullet.Fitted += OnAddCount;
            bullet.Removed += OnUnsubscribe;
        }

        private void OnUnsubscribe(Bullet bullet)
        {
            bullet.Fitted -= OnAddCount;
            bullet.Removed -= OnUnsubscribe;
        }

        private void OnAddCount(int score)
        {
            CurrentScore += score;
            _scoreView.ShowScore(CurrentScore);
        }
    }
}