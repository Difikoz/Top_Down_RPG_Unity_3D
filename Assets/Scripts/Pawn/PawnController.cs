using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        [SerializeField] private bool _autoWork;

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
            if (_autoWork)
            {
                Initialize();
            }
        }

        private void Update()
        {
            if (_autoWork)
            {
                OnUpdate();
            }
        }

        public void Initialize()
        {
            CreatePawn();
            GetComponents();
            InitializeComponents();
        }

        private void CreatePawn()
        {
            if (_animator != null)
            {
                Destroy(_animator.gameObject);// despawn model
            }
            // spawn model
        }

        private void GetComponents()
        {
            _animator = GetComponentInChildren<PawnAnimator>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
        }

        private void InitializeComponents()
        {
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