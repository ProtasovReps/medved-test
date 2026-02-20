using System;
using Extensions;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Panel
{
    public class CameraRotationPanel : MonoBehaviour, IRotationInput, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RotationSides _side;

        public event Action<int> Rotating;
        public event Action Stopped;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Rotating?.Invoke((int)_side);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Stopped?.Invoke();
        }
    }
}