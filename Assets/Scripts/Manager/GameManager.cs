using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerManager _playerManager;
        private UIManager _uiManager;
        private CameraManager _cameraManager;
        private LayerManager _layerManager;

        public PlayerManager PlayerManager => _playerManager;
        public UIManager UIManager => _uiManager;
        public CameraManager CameraManager => _cameraManager;
        public LayerManager LayerManager => _layerManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _playerManager = GetComponentInChildren<PlayerManager>();
            _uiManager = GetComponentInChildren<UIManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _layerManager = GetComponentInChildren<LayerManager>();
        }

        private void InitializeComponents()
        {
            _playerManager.Initialize();
            _cameraManager.Initialize();
        }

        private void Update()
        {
            _playerManager.OnUpdate();
            _cameraManager.OnUpdate();
        }
    }
}