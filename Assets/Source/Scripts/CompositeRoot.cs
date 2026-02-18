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

        private Disposer _disposer;

        private void Start()
        {
            _disposer = new Disposer();

            InstallTargets();
            InstallCamera();
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

        private void InstallCamera()
        {
            InputActions actions = new();
            MoveInputReader reader = new(actions);
            CameraMovement cameraMovement = new(reader, _cameraSpeed);

            _disposer.Add(actions);
            _disposer.Add(reader);
            _disposer.Add(cameraMovement);

            actions.Enable();
        }
    }
}