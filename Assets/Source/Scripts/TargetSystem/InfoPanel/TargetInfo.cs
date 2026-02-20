using System;
using Interface;
using TMPro;
using UnityEngine;

namespace TargetSystem.InfoPanel
{
    [Serializable]
    public class TargetInfo
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _position;

        public void Update(IInformationalTarget target)
        {
            _name.text = target.Name;
            _position.text = target.Position.ToString();
        }
    }
}