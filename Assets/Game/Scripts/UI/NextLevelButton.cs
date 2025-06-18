using UnityEngine.SceneManagement;
using YG;

namespace Scripts.UI
{
    public class NextLevelButton : ButtonHandler
    {
        protected override void OnButtonClick()
        {
            YG2.saves.LevelNumber++;
            YG2.SaveProgress();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            YG2.InterstitialAdvShow();
        }
    }
}