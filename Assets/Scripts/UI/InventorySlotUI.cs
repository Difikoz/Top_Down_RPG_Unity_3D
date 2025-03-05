using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _amountText;

        private ItemConfig _item;
        private int _amount;

        public void Initialize(ItemConfig s)
        {
            _item = s;
            _iconImage.sprite = _item.Icon;
            _amountText.text = $"{(_amount > 1 ? _amount : "")}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameManager.StaticInstance.UIManager.StatusBar.InventoryBar.ShowFullInformation(_item);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _item.Use(GameManager.StaticInstance.PlayerManager.Pawn);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _item.Use(GameManager.StaticInstance.PlayerManager.Pawn);
        }
    }
}