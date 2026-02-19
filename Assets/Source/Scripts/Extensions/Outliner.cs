using System;
using UnityEngine;

namespace Extensions
{
    [Serializable]
    public class Outliner
    {
        private const string OutlineProperty = "_OtlWidth";

        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _enabledWidth;
        [SerializeField] private float _disabledWidth;

        private MaterialPropertyBlock _propertyBlock;

        public void Initialize()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void Enable()
        {
            SetBlock(_enabledWidth);
        }

        public void Disable()
        {
            SetBlock(_disabledWidth);
        }

        private void SetBlock(float value)
        {
            _propertyBlock.SetFloat(OutlineProperty, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}