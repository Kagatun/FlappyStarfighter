using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void ShowScore(int score) =>
            _text.text = score.ToString();
    }
}