using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class SceneSwitcher : ButtonHandler
    {
        [SerializeField] private int _sceneIndex;

        protected override void OnButtonClick() =>
            SceneManager.LoadScene(_sceneIndex);
    }
}