using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.Systems
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Image _desctop;
        [SerializeField] private Image _mobile;

        private void Start()
        {
            if (YG2.saves.IsDesktop)
                _desctop.gameObject.SetActive(true);
            else
                _mobile.gameObject.SetActive(true);
        }
    }
}
