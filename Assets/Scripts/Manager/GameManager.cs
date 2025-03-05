using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerManager _playerManager;
        private AIManager _aiManager;
        private UIManager _uiManager;
        private CameraManager _cameraManager;
        private ConfigsManager _configsManager;
        private LayerManager _layerManager;

        public PlayerManager PlayerManager => _playerManager;
        public AIManager AIManager => _aiManager;
        public UIManager UIManager => _uiManager;
        public CameraManager CameraManager => _cameraManager;
        public ConfigsManager ConfigsManager => _configsManager;
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
            _aiManager = GetComponentInChildren<AIManager>();
            _uiManager = GetComponentInChildren<UIManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _layerManager = GetComponentInChildren<LayerManager>();
        }

        private void InitializeComponents()
        {
            _configsManager.Initialize();
            _playerManager.Initialize();
            _aiManager.Initialize();
            _cameraManager.Initialize();
        }

        private void Update()
        {
            _playerManager.OnUpdate();
            _aiManager.OnUpdate();
            _cameraManager.OnUpdate();
        }
    }
}