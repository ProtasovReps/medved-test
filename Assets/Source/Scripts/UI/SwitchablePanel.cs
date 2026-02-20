using UnityEngine;

namespace UI
{
    public class SwitchablePanel : MonoBehaviour
    {
        [field: SerializeField] public TargetInfoPanel Panel { get; private set; }
        [field: SerializeField] public TargetButton ExitButton { get; private set; }
    }
}