using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.UI
{
    public class SwitcherUI : ButtonHandler
    {
        [SerializeField] private List<Image> _panelsOpen;
        [SerializeField] private List<Image> _panelsClose;

        protected override void OnButtonClick()
        {
            YG2.InterstitialAdvShow();
            
            foreach (Image panel in _panelsOpen)
                panel.gameObject.SetActive(true);

            foreach (Image panel in _panelsClose)
                panel.gameObject.SetActive(false);
        }
    }
}