using UI.Button;
using UnityEngine;

namespace UI.Panel
{
    public class SwitchablePanel : MonoBehaviour
    {
        [field: SerializeField] public TargetInfoPanel Panel { get; private set; }
        [field: SerializeField] public TargetButton ExitButton { get; private set; }
    }
}