using UnityEngine;

namespace WinterUniverse
{
    public class UIManager : MonoBehaviour
    {
        private HUDUI _hud;
        private StatusBarUI _statusBar;

        public void Initialize()
        {
            GetComponents();
            InitializeComponents();
        }

        public void ResetComponent()
        {
            _hud.ResetComponent();
            _statusBar.ResetComponent();
        }

        private void GetComponents()
        {
            _hud = GetComponentInChildren<HUDUI>();
            _statusBar = GetComponentInChildren<StatusBarUI>();
        }

        private void InitializeComponents()
        {
            _hud.Initialize();
            _statusBar.Initialize();
        }

        public void OnStatusBar()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.gameObject.SetActive(false);
            }
            else
            {
                _statusBar.gameObject.SetActive(true);
            }
        }

        public void OnToggleHUD()
        {
            if (_hud.isActiveAndEnabled)
            {
                _hud.gameObject.SetActive(false);
            }
            else
            {
                _hud.gameObject.SetActive(true);
            }
        }

        public void OnPreviousTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.TabGroup.PreviousTab();
            }
        }

        public void OnNextTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.TabGroup.NextTab();
            }
        }
    }
}