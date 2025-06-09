using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class RestarterLevel : ButtonHandler
    {
        protected override void OnButtonClick() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
