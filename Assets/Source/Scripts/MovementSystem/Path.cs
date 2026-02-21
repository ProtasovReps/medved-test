using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace MovementSystem
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] _pathPoints;

        private int _currentIndex;

        public IEnumerable<Vector3> PathPoints => _pathPoints.Select(point => point.position);

        private void OnDrawGizmos()
        {
            if (_pathPoints == null)
            {
                return;
            }

            Gizmos.color = Color.skyBlue;

            for (int i = 0; i < _pathPoints.Length; i++)
            {
                _currentIndex = _pathPoints.GetCycledIndex(_currentIndex);

                Gizmos.DrawLine(_pathPoints[i].position, _pathPoints[_currentIndex].position);
            }
        }
    }
}