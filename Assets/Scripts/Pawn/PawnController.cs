using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private bool _created;
        private PawnData _data;
        private PawnAnimator _animator;
        private PawnCombat _combat;
        private PawnDetection _detection;
        private PawnEquipment _equipment;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;
        private PawnFaction _faction;
        private PawnStatus _status;
        private PawnEffects _effects;
        //
        private StateHolder _stateHolder;

        public bool Created => _created;
        public PawnData Data => _data;
        public PawnAnimator Animator => _animator;
        public PawnCombat Combat => _combat;
        public PawnDetection Detection => _detection;
        public PawnEquipment Equipment => _equipment;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnFaction Faction => _faction;
        public PawnStatus Status => _status;
        public PawnEffects Effects => _effects;
        //
        public StateHolder StateHolder => _stateHolder;

        public void Initialize(PawnData data)
        {
            ResetComponent();
            _data = new()
            {
                DisplayName = data.DisplayName,
                Visual = data.Visual,
                Faction = data.Faction,
                Inventory = data.Inventory,
                StateHolder = data.StateHolder
            };
            LeanPool.Spawn(GameManager.StaticInstance.ConfigsManager.GetVisual(_data.Visual).Model, transform);
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
                LeanPool.Despawn(transform.GetChild(0).gameObject);
                _created = false;
            }
        }

        private void GetComponents()
        {
            _animator = GetComponentInChildren<PawnAnimator>();
            _combat = GetComponent<PawnCombat>();
            _detection = GetComponentInChildren<PawnDetection>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _faction = GetComponent<PawnFaction>();
            _status = GetComponent<PawnStatus>();
            _effects = GetComponent<PawnEffects>();
        }

        private void InitializeComponents()
        {
            _stateHolder = new();
            foreach (StateConfig config in GameManager.StaticInstance.ConfigsManager.PawnStates)
            {
                _stateHolder.SetState(config.ID, false);
            }
            foreach (StateCreator creator in GameManager.StaticInstance.ConfigsManager.GetStateHolder(_data.StateHolder).StatesToChange)
            {
                _stateHolder.SetState(creator.Config.ID, creator.Value);
            }
            _animator.Initialize();
            _combat.Initialize();
            _detection.Initialize();
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
                _detection.OnUpdate();
                _combat.OnUpdate();
                _locomotion.OnUpdate();
                _status.OnUpdate();
                _effects.OnUpdate();
            }
        }
    }
}