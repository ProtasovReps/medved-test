using Interface;
using UnityEngine;

namespace InputSystem.UI
{
    [RequireComponent(typeof(Canvas))]
    public class TargetInfoPanel : MonoBehaviour
    {
        private IInformationalTarget _target;
        
        public void SetTarget(IInformationalTarget target)
        {
            _target = target;
        }
    }
}