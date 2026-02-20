using Extensions;
using MovementSystem;
using TargetSystem;
using TargetSystem.Adapter;
using TargetSystem.InfoPanel;
using TargetSystem.Notifier;
using UI;
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
        [SerializeField] private SwitchablePanel _prefab;
        
        [Header("Effects")]
        [SerializeField] private LineRenderer _lineRenderer;
        
        private ClickReader _clickReader;
        private MoveInputReader _moveReader;
        private Disposer _disposer;
        private InfoPanelPool _pool;
        
        private void Start()
        {
            _disposer = new Disposer();

            InputActions actions = InstallInput();
            
            InstallTargets();
            InstallCamera();
            
            SelectionNotifier notifier = InstallSelectNotification();
            
            InstallPanelSwitch(notifier);
            InstallRope(notifier);
            InstallOutline(notifier);
            
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

        private SelectionNotifier InstallSelectNotification()
        {
            _pool = new InfoPanelPool(_prefab);
            
            TargetRaycaster raycaster = new (_clickReader, _targetsLayer);
            SelectionNotifier selectionNotifier = new SelectionNotifier(raycaster);
            NotifierExitButtonAdder adder = new NotifierExitButtonAdder(_pool, selectionNotifier);
             
            _disposer.Add(raycaster);
            _disposer.Add(selectionNotifier);
            _disposer.Add(adder);
            return selectionNotifier;
        }

        private void InstallPanelSwitch(SelectionNotifier selectionNotifier)
        {
            InfoPanelSwitcher switcher = new InfoPanelSwitcher(selectionNotifier, _pool);
            
            _disposer.Add(switcher);
        }

        private void InstallRope(SelectionNotifier selectionNotifier)
        {
            PositionableAdapter adapter = new(selectionNotifier);
            Rope rope = new(_lineRenderer, adapter);
            
            _disposer.Add(adapter);
            _disposer.Add(rope);
        }
        
        private void InstallOutline(SelectionNotifier selectionNotifier)
        {
            OutlineAdapter adapter = new(selectionNotifier);
            OutlineSwitcher switcher = new(adapter);
            
            _disposer.Add(adapter);
            _disposer.Add(switcher);
        }
    }
}