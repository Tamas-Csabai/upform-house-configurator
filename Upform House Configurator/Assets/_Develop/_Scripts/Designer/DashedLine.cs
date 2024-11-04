
using UnityEngine;

namespace Upform.Designer
{
    public class DashedLine : MonoBehaviour
    {

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private void Awake()
        {
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }

        private void LateUpdate()
        {
            if (startPoint.hasChanged)
            {
                lineRenderer.SetPosition(0, startPoint.position);
            }

            if (endPoint.hasChanged)
            {
                lineRenderer.SetPosition(1, endPoint.position);
            }
        }

        public void SetStartPoint(Vector3 position)
        {
            startPoint.transform.position = position;
        }

        public void SetEndPoint(Vector3 position)
        {
            endPoint.transform.position = position;
        }

    }
}
