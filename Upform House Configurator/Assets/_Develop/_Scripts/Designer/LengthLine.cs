
using TMPro;
using UnityEngine;

namespace Upform.Designer
{
    public class LengthLine : MonoBehaviour
    {

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private Vector3 _prevVector;

        private void Awake()
        {
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);

            _prevVector = endPoint.position - startPoint.position;

            textMeshPro.transform.position = startPoint.position + (_prevVector / 2f);
            textMeshPro.text = _prevVector.magnitude.ToString("0.00");
        }

        private void LateUpdate()
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);

            Vector3 vector = endPoint.position - startPoint.position;

            if(vector != _prevVector)
            {
                _prevVector = vector;
                textMeshPro.transform.position = startPoint.position + (vector / 2f);
                textMeshPro.text = vector.magnitude.ToString("0.00");
            }
        }

    }
}
