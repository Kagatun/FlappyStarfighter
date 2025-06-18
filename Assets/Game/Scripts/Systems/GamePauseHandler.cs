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
            if (FocusObserver.HasFocus == false)
                OnPauseGame();
            else
                OnUnPauseGame();
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
            if (_imagesPause.Count == 0 || _imagesPause == null)
            {
                return false;
            }

            foreach (var image in _imagesPause)
            {
                if (image != null && image.gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnYandexVisibilityChanged(bool visible)
        {
            if (visible)
            {
                if (ShouldStayPaused() == false)
                    OnUnPauseGame();
            }
            else
                OnPauseGame();
        }

        private void OnFocus(bool hasFocus)
        {
            if (FocusObserver.IsTransitioning)
            {
                if (hasFocus == false)
                    OnPauseGame();
                else
                    OnUnPauseGame();
                
                return;
            }

            if (hasFocus == false)
                OnPauseGame();
            else
            {
                if (!ShouldStayPaused())
                    OnUnPauseGame();
            }
        }

        private void OnPause(bool pauseStatus)
        {
            if (FocusObserver.IsTransitioning)
            {
                if (pauseStatus)
                    OnPauseGame();
                else
                    OnUnPauseGame();

                return;
            }

            if (pauseStatus)
                OnPauseGame();
            else
            {
                if (!ShouldStayPaused())
                    OnUnPauseGame();
            }
        }

        private void OnPauseGame()
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        private void OnUnPauseGame()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
        }
    }
}

