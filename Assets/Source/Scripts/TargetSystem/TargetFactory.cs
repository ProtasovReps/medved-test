using Extensions;
using MovementSystem;
using UnityEngine;

namespace TargetSystem
{
    public class TargetFactory
    {
        private readonly Disposer _disposer;
        private readonly Transform _targetParent;
        private readonly Transform _pathParent;

        public TargetFactory(Disposer disposer, Transform targetParent, Transform pathParent)
        {
            _disposer = disposer;
            _targetParent = targetParent;
            _pathParent = pathParent;
        }

        public void Produce(TargetConfig config, Vector3 position)
        {
            Target target = Object.Instantiate(config.Prefab, position, Quaternion.identity, _targetParent);
            Path path = Object.Instantiate(config.Path, position, Quaternion.identity, _pathParent);
            CycledMovement movement = new (target.transform, path, config.MoveSpeed);

            target.Initialize(config.Name);

            _disposer.Add(movement);
        }
    }
}