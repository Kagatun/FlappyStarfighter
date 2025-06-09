using Scripts.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using YG;

namespace Scripts.UI
{
    public class LevelButton : ButtonHandler
    {
        [SerializeField] private GameSettings _levelSettings;
        [SerializeField] private Image _imageBlock;
        [SerializeField] private Image _imageButton;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            if (YG2.saves.LevelIndex < _levelSettings.LevelNumber)
            {
                _imageBlock.gameObject.SetActive(true);
                _text.gameObject.SetActive(false);
                ActionButton.interactable = false;;
            }

            _imageButton.color = _levelSettings.SpaceColor;
            _text.text = _levelSettings.LevelNumber.ToString();
        }

        protected override void OnButtonClick()
        {
            LevelManager.GameSettings = _levelSettings;
            SceneManager.LoadScene(1);
        }
    }
}