using MovementSystem;
using UnityEngine;

namespace TargetSystem
{
    [CreateAssetMenu(fileName = nameof(TargetConfig))]
    public class TargetConfig : ScriptableObject
    {
        [field: SerializeField] public Target Prefab { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public Path Path { get; private set; }
    }
}