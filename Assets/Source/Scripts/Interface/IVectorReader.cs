using System;
using UnityEngine;

namespace Interface
{
    public interface IVectorReader
    {
        public event Action<Vector2> ValueChanged;
    }
}