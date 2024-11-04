
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Upform.Designer
{
    public class LineVisual : MonoBehaviour
    {

        private const string COLOR_PROPERTY_NAME = "_BaseColor";

        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Color defaultColor = Color.white;

        private Vector3 _prevVector;

        public Transform StartPoint => startPoint;
        public Transform EndPoint => endPoint;

        private void Awake()
        {
            SetColor(defaultColor);

            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);

            _prevVector = endPoint.position - startPoint.position;

            textMeshPro.transform.position = startPoint.position + (_prevVector / 2f);
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
            }
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }

        public void SetTextEnabled(bool enabled)
        {
            textMeshPro.enabled = enabled;
        }

        public void SetColor(Color color)
        {
            Material lineMaterial = lineRenderer.material;

            lineMaterial.SetColor(COLOR_PROPERTY_NAME, color);

            lineRenderer.material = lineMaterial;
        }

    }
}
