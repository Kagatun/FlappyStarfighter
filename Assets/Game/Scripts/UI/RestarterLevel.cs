using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Scripts.UI
{
    public class RestarterLevel : ButtonHandler
    {
        protected override void OnButtonClick()
        {
            YG2.InterstitialAdvShow();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
