using System;
using TMPro;
using UnityEngine;

namespace TargetSystem.Info
{
    [Serializable]
    public class TargetInfo
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _position;

        public void Update(string name, Vector3 position)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _name.text = name;
            _position.text = position.ToString();
        }
    }
}