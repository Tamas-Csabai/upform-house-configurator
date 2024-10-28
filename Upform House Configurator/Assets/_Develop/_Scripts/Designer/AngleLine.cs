using TMPro;
using UnityEngine;

namespace Upform.Designer
{
    public class AngleLine : MonoBehaviour
    {
        public static int ObjectCount = 0;

        private const float ANGLE_ARC_DETAILNESS = 0.2f;

        [SerializeField] private Transform crossPoint;
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private TextMeshPro lengthText;
        [SerializeField] private LineRenderer lineRenderer1;
        [SerializeField] private LineRenderer lineRenderer2;
        [SerializeField] private LineRenderer angleLineRenderer;

        public Transform CrossPoint => crossPoint;
        public Transform Point1 => point1;
        public Transform Point2 => point2;

        private void Awake()
        {
            ObjectCount++;

            lineRenderer1.loop = false;
            lineRenderer2.loop = false;
            angleLineRenderer.loop = false;

            lineRenderer1.SetPosition(0, crossPoint.position);
            lineRenderer1.SetPosition(1, point1.position);

            lineRenderer2.SetPosition(0, crossPoint.position);
            lineRenderer2.SetPosition(1, point2.position);
        }

        private void LateUpdate()
        {
            if (!transform.hasChanged && !point1.hasChanged && !point2.hasChanged && !crossPoint.hasChanged)
                return;

            lineRenderer1.SetPosition(0, crossPoint.position);
            lineRenderer1.SetPosition(1, point1.position);

            lineRenderer2.SetPosition(0, crossPoint.position);
            lineRenderer2.SetPosition(1, point2.position);

            Vector3 vector1 = Vector3.ProjectOnPlane(point1.position - crossPoint.position, Vector3.up);
            Vector3 vector2 = Vector3.ProjectOnPlane(point2.position - crossPoint.position, Vector3.up);

            Vector3 axis1 = vector1.normalized;
            Vector3 axis2 = vector2.normalized;

            float angle = Vector3.Angle(axis1, axis2);

            lengthText.text = angle.ToString("0.00") + "°";

            float angleInDeg = Mathf.Deg2Rad * angle;

            angleLineRenderer.positionCount = Mathf.CeilToInt(angleInDeg / ANGLE_ARC_DETAILNESS) + 1;

            Vector3 angleForward = Vector3.Cross(axis1, axis2);

            Quaternion lookRotation = Quaternion.LookRotation(angleForward, Vector3.Cross(axis1, angleForward));

            float radius = Mathf.Min(Mathf.Min(vector1.magnitude - 0.1f, vector2.magnitude - 0.1f), 0.5f);

            int arcPointIndex = 0;

            for (float i = 0; i < angleInDeg; i += ANGLE_ARC_DETAILNESS)
            {
                float x = radius * Mathf.Cos(i);
                float y = radius * Mathf.Sin(i);

                Vector3 pos = crossPoint.position + (lookRotation * new Vector3(-x, -y, 0));
                angleLineRenderer.SetPosition(arcPointIndex, pos);

                if (i == ANGLE_ARC_DETAILNESS * 2f)
                {
                    lengthText.transform.position = crossPoint.position + (1 * (lookRotation * new Vector3(-x, -y, 0)).normalized);
                }

                arcPointIndex++;
            }

            float lastX = radius * Mathf.Cos(angleInDeg);
            float lastY = radius * Mathf.Sin(angleInDeg);

            Vector3 lastPos = crossPoint.position + (lookRotation * new Vector3(-lastX, -lastY, 0));
            angleLineRenderer.SetPosition(arcPointIndex, lastPos);
        }
    }
}
