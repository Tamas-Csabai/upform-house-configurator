using UnityEngine;

namespace Upform.Designer
{
    public class LengthLine : MonoBehaviour
    {

        private const string LENGTH_FORMAT = "0.00";

        [SerializeField] private LineVisual lineVisual;

        private Vector3 _prevLineVector;

        private void Awake()
        {
            _prevLineVector = lineVisual.EndPoint.position - lineVisual.StartPoint.position;
        }

        private void Start()
        {
            lineVisual.SetText(_prevLineVector.magnitude.ToString(LENGTH_FORMAT));
        }

        private void Update()
        {
            Vector3 lineVector = lineVisual.EndPoint.position - lineVisual.StartPoint.position;

            if (_prevLineVector != lineVector)
            {
                _prevLineVector = lineVector;

                lineVisual.SetText(lineVector.magnitude.ToString(LENGTH_FORMAT));
            }
        }

    }
}
