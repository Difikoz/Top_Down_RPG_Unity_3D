using UnityEngine;

namespace WinterUniverse
{
    public class StatusBarUI : MonoBehaviour
    {
        private TabGroup _tabGroup;
        private StatsBarUI _statsBar;

        public TabGroup TabGroup => _tabGroup;
        public StatsBarUI StatsBar => _statsBar;

        public void Initialize()
        {
            _tabGroup = GetComponent<TabGroup>();
            _statsBar = GetComponentInChildren<StatsBarUI>();
            _tabGroup.Initialize();
            _statsBar.Initialize();
            gameObject.SetActive(false);
        }

        public void ResetComponent()
        {
            _statsBar.ResetComponent();
        }
    }
}