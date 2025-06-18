using System;
using Scripts.Systems;
using TMPro;
using UnityEngine;
using YG;

namespace Scripts.Shop
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public event Action GoldChanged;

        private void Start()
        {
            ShowGold();
        }

        public void ShowGold()
        {
            GoldChanged?.Invoke();
            _text.text = YG2.saves.Gold.ToString();
        }
    }
}