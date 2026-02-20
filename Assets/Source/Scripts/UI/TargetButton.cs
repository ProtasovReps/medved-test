using System;
using Interface;
using TargetSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TargetButton : MonoBehaviour, IObjectNotifier<Target>
    {
        [SerializeField] private Button _button;

        private Target _target;

        public event Action<Target> Notified;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnPressed);
        }

        public void Set(Target target)
        {
            _target = target;
        }
        
        private void OnPressed()
        {
            if (_target == null)
            {
                throw new NullReferenceException(nameof(_target));
            }

            Notified?.Invoke(_target);
        }
    }
}