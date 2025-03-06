using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private bool _created;
        private PawnConfig _config;
        private PawnAnimator _animator;
        private PawnCombat _combat;
        private PawnEquipment _equipment;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;
        private PawnFaction _faction;
        private PawnStatus _status;
        private PawnEffects _effects;

        public bool Created => _created;
        public PawnConfig Config => _config;
        public PawnAnimator Animator => _animator;
        public PawnCombat Combat => _combat;
        public PawnEquipment Equipment => _equipment;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnFaction Faction => _faction;
        public PawnStatus Status => _status;
        public PawnEffects Effects => _effects;

        public void Initialize(PawnConfig config)
        {
            ResetComponent();
            _config = config;
            LeanPool.Spawn(_config.Model, transform);
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
                _combat.ResetComponent();
                _status.ResetComponent();
                LeanPool.Despawn(transform.GetChild(0).gameObject);
                _created = false;
            }
        }

        private void GetComponents()
        {
            _animator = GetComponentInChildren<PawnAnimator>();
            _combat = GetComponent<PawnCombat>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _faction = GetComponent<PawnFaction>();
            _status = GetComponent<PawnStatus>();
            _effects = GetComponent<PawnEffects>();
        }

        private void InitializeComponents()
        {
            _animator.Initialize();
            _combat.Initialize();
            _equipment.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
            _faction.Initialize();
            _status.Initialize();
            _effects.Initialize();
            _created = true;
        }

        public void OnUpdate()
        {
            if (_created)
            {
                _animator.OnUpdate();
                _combat.OnUpdate();
                _locomotion.OnUpdate();
                _status.OnUpdate();
                _effects.OnUpdate();
            }
        }
    }
}