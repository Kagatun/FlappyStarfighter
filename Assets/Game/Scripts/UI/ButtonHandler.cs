using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.UI
{
    public abstract class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        
        public Button ActionButton => _actionButton;

        private void OnEnable()
        {
            _actionButton.onClick.AddListener(OnButtonClick);
            OnEnableAction();
        }

        private void OnDisable()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
            OnDisableAction();
        }

        protected abstract void OnButtonClick();

        protected virtual void OnEnableAction()
        {
        }

        protected virtual void OnDisableAction()
        {
        }
    }
}