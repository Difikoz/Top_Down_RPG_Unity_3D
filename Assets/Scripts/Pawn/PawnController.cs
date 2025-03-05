using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _animator;
        private PawnEquipment _equipment;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;

        public PawnAnimator Animator => _animator;
        public PawnEquipment Equipment => _equipment;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;

        private void Awake()
        {
            Initialize();// for test
        }

        public void Initialize()
        {
            // spawn model
            _animator = GetComponentInChildren<PawnAnimator>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _animator.Initialize();
            _equipment.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
        }

        public void OnUpdate()
        {
            _animator.OnUpdate();
            _locomotion.OnUpdate();
        }
    }
}