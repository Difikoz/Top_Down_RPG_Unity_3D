using UnityEngine;

namespace WinterUniverse
{
    public class StatusBarUI : MonoBehaviour
    {
        private TabGroup _tabGroup;
        private StatsBarUI _statsBar;
        private InventoryBarUI _inventoryBar;

        public TabGroup TabGroup => _tabGroup;
        public StatsBarUI StatsBar => _statsBar;
        public InventoryBarUI InventoryBar => _inventoryBar;

        public void Initialize()
        {
            _tabGroup = GetComponent<TabGroup>();
            _statsBar = GetComponentInChildren<StatsBarUI>();
            _inventoryBar = GetComponentInChildren<InventoryBarUI>();
            _tabGroup.Initialize();
            _statsBar.Initialize();
            _inventoryBar.Initialize();
            gameObject.SetActive(false);
        }

        public void ResetComponent()
        {
            _statsBar.ResetComponent();
            _inventoryBar.ResetComponent();
        }
    }
}