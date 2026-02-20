using UnityEngine;

namespace UI
{
    public class SwitchablePanel : MonoBehaviour
    {
        [field: SerializeField] public TargetInfoPanel InfoPanel;
        [field: SerializeField] public TargetButton ExitButton;
    }
}