using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Scripts.UI
{
    public class SceneSwitcher : ButtonHandler
    {
        [SerializeField] private int _sceneIndex;

        protected override void OnButtonClick()
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}