using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Systems
{
    public class GamePauseHandler : MonoBehaviour
    {
        [SerializeField] private List<Image> _imagesPause;

        private void Start()
        {
            SetPauseState(!FocusObserver.HasFocus);
        }

        private void OnEnable()
        {
            FocusObserver.ApplicationFocus += OnFocus;
            FocusObserver.ApplicationPause += OnPause;
            YG2.onPauseGame += OnYandexVisibilityChanged;
        }

        private void OnDisable()
        {
            FocusObserver.ApplicationFocus -= OnFocus;
            FocusObserver.ApplicationPause -= OnPause;
            YG2.onPauseGame -= OnYandexVisibilityChanged;
        }

        private bool ShouldStayPaused()
        {
            if (_imagesPause == null || _imagesPause.Count == 0)
                return false;

            foreach (var image in _imagesPause)
            {
                if (image != null && image.gameObject.activeSelf)
                    return true;
            }

            return false;
        }

        private void OnYandexVisibilityChanged(bool visible)
        {
            if (visible)
            {
                if (!ShouldStayPaused())
                    SetPauseState(false);
            }
            else
            {
                SetPauseState(true);
            }
        }

        private void OnFocus(bool hasFocus)
        {
            SetPauseState(!hasFocus);
        }

        private void OnPause(bool pauseStatus)
        {
            SetPauseState(pauseStatus);
        }

        private void SetPauseState(bool wantPause)
        {
            if (wantPause)
            {
                PauseGame();
            }
            else
            {
                if (!ShouldStayPaused())
                    UnPauseGame();
            }
        }

        private void PauseGame()
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        private void UnPauseGame()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}