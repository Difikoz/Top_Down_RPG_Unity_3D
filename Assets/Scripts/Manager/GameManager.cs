using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private InputMode _inputMode;
        private PlayerManager _playerManager;
        private AIManager _aiManager;
        private CameraManager _cameraManager;
        private AudioManager _audioManager;
        private ConfigsManager _configsManager;
        private LayerManager _layerManager;
        private PrefabsManager _prefabsManager;
        private UIManager _uiManager;

        public InputMode InputMode => _inputMode;
        public PlayerManager PlayerManager => _playerManager;
        public AIManager AIManager => _aiManager;
        public CameraManager CameraManager => _cameraManager;
        public AudioManager AudioManager => _audioManager;
        public ConfigsManager ConfigsManager => _configsManager;
        public LayerManager LayerManager => _layerManager;
        public PrefabsManager PrefabsManager => _prefabsManager;
        public UIManager UIManager => _uiManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        //private void OnDestroy()
        //{
        //    _uiManager.ResetComponent();
        //    _cameraManager.ResetComponent();
        //    _playerManager.ResetComponent();
        //    _aiManager.ResetComponent();
        //}

        private void GetComponents()
        {
            _playerManager = GetComponentInChildren<PlayerManager>();
            _aiManager = GetComponentInChildren<AIManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _audioManager = GetComponentInChildren<AudioManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _layerManager = GetComponentInChildren<LayerManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
            _uiManager = GetComponentInChildren<UIManager>();
        }

        private void InitializeComponents()
        {
            _aiManager.Initialize();
            _configsManager.Initialize();
            _playerManager.Initialize();
            _aiManager.Initialize();
            _cameraManager.Initialize();
            _uiManager.Initialize();
        }

        private void Update()
        {
            _playerManager.OnUpdate();
            _aiManager.OnUpdate();
            _cameraManager.OnUpdate();
        }

        public void SetInputMode(InputMode mode)
        {
            _inputMode = mode;
        }
    }
}