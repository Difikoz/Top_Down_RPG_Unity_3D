using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private bool _created;
        private PawnConfig _config;
        private PawnAnimator _animator;
        private PawnEquipment _equipment;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;
        private PawnStatus _status;

        public bool Created => _created;
        public PawnConfig Config => _config;
        public PawnAnimator Animator => _animator;
        public PawnEquipment Equipment => _equipment;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnStatus Status => _status;

        public void Initialize(PawnConfig config)
        {
            ResetComponent();
            _config = config;
            CreatePawn();
            GetComponents();
            InitializeComponents();
        }

        public void ResetComponent()
        {
            if (_created)
            {
                _animator.ResetComponent();
                _equipment.ResetComponent();
                _inventory.ResetComponent();
                _locomotion.ResetComponent();
                _status.ResetComponent();
                LeanPool.Despawn(_animator.gameObject);
                _created = false;
            }
        }

        private void CreatePawn()
        {
            LeanPool.Spawn(_config.Model, transform);
        }

        private void GetComponents()
        {
            _animator = GetComponentInChildren<PawnAnimator>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _status = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _animator.Initialize();
            _equipment.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
            _status.Initialize();
            _created = true;
        }

        public void OnUpdate()
        {
            if (_created)
            {
                _animator.OnUpdate();
                _locomotion.OnUpdate();
                _status.OnUpdate();
            }
        }
    }
}