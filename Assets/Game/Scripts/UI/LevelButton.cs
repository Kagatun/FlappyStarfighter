using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using YG;

namespace Scripts.UI
{
    public class LevelButton : ButtonHandler
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Image _imageBlock;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _buttonImage.color = _backgroundColor;
            
            if (YG2.saves.LevelIndex < _levelNumber)
            {
                _imageBlock.gameObject.SetActive(true);
                _text.gameObject.SetActive(false);
                ActionButton.interactable = false;

                return;
            }
            
            _text.text = _levelNumber.ToString();
        }

        protected override void OnButtonClick()
        {
            YG2.saves.LevelNumber = _levelNumber - 1;
            YG2.SaveProgress();
            SceneManager.LoadScene(1);
        }
    }
}