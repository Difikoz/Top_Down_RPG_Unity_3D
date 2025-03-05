using UnityEngine;

namespace WinterUniverse
{
    public class StatusBarUI : MonoBehaviour
    {
        private TabGroup _tabGroup;

        public TabGroup TabGroup => _tabGroup;

        public void Initialize()
        {
            _tabGroup = GetComponent<TabGroup>();
            _tabGroup.Initialize();
            gameObject.SetActive(false);
        }

        public void ResetComponent()
        {

        }
    }
}