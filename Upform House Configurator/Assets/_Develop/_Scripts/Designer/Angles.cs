using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Upform.Designer
{
    public class Angles : MonoBehaviour
    {

        [SerializeField] private AngleLine angleLine;

        private float _step;

        private Vector3[] _stepDirections;

        public float Step
        {
            get => _step;
            set
            {
                _step = value;

                if(_step < 1f || _step >= 360f)
                {
                    _step = 0f;
                    return;
                }

                int stepCount = Mathf.CeilToInt(360f / _step);

                _stepDirections = new Vector3[stepCount];

                for (int i = 0; i < _stepDirections.Length; i++)
                {
                    _stepDirections[i] = Quaternion.Euler(0, i * _step, 0) * Vector3.forward;
                }
            }
        }

        private void Awake()
        {
            angleLine.gameObject.SetActive(false);
        }

        public Vector3 WorldToAngleOnPlane(Vector3 origin, Vector3 worldPosition)
        {
            if(_step == 0)
            {
                return worldPosition;
            }

            Vector3 direction = (worldPosition - origin).normalized;

            float minAngle = float.MaxValue;
            Vector3 closestDirection = Vector3.zero;

            for (int i = 0; i < _stepDirections.Length; i++)
            {
                float angle = Vector3.Angle(direction, _stepDirections[i]);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    closestDirection = _stepDirections[i];
                }
            }

            Vector3 closestPoint = Utils.FindClosestPositionOnLine(origin, closestDirection, worldPosition);
            
            return closestPoint;
        }

        public void SetLine(Vector3 crossPoint, Vector3 point1, Vector3 point2)
        {
            angleLine.CrossPoint.position = crossPoint;
            angleLine.Point1.position = point1;
            angleLine.Point2.position = point2;
        }

        public void SetLineActive(bool isActive)
        {
            angleLine.gameObject.SetActive(isActive);
        }

        /*
        private void OnDrawGizmos()
        {
            if(_stepDirections != null)
            {
                for (int i = 0; i < _stepDirections.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(Vector3.zero, _stepDirections[i]);
                }
            }
        }
        */
    }
}
