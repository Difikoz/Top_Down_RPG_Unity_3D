using UnityEngine;

namespace WinterUniverse
{
    public class ItemConfig : BasicInfoConfig
    {
        [SerializeField] private GameObject _model;
        [SerializeField] private float _weight = 1f;
        [SerializeField] private int _price = 100;

        public GameObject Model => _model;
        public float Weight => _weight;
        public int Price => _price;
    }
}