using Extensions;
using MovementSystem;
using TargetSystem;
using UnityEngine;

namespace InputSystem
{
    public class CompositeRoot : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private float _cameraSpeed;
        
        [Header("Targets")]
        [SerializeField] private Transform _targetParent;
        [SerializeField] private Transform _pathParent;
        [SerializeField] private TargetConfig[] _targetConfigs;
        [SerializeField] private Transform[] _spawnPoints;

        [Header("UI")]
        [SerializeField] private LayerMask _targetsLayer;
        
        private ClickReader _clickReader;
        private MoveInputReader _moveReader;
        private Disposer _disposer;

        private void Start()
        {
            _disposer = new Disposer();

            InputActions actions = InstallInput();
            
            InstallTargets();
            InstallCamera();
            InstallUI();
            
            actions.Enable();
        }

        private void OnDestroy()
        {
            _disposer.Dispose();
        }

        private void InstallTargets()
        {
            TargetFactory targetFactory = new(_disposer, _targetParent, _pathParent);

            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                targetFactory.Produce(_targetConfigs[i], _spawnPoints[i].position);
            }
        }
        
        private InputActions InstallInput()
        {
            InputActions actions = new();
            
            _clickReader = new(actions);
            _moveReader = new(actions);
            
            _disposer.Add(actions);
            _disposer.Add(_moveReader);
            _disposer.Add(_clickReader);
            return actions;
        }
        
        private void InstallCamera()
        {
            CameraMovement cameraMovement = new(_moveReader, _cameraSpeed);

            _disposer.Add(cameraMovement);
        }

        private void InstallUI()
        {
            TargetRaycaster raycaster = new(_clickReader, _targetsLayer);
            
            _disposer.Add(raycaster);
        }
    }
}