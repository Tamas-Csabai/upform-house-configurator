using UnityEngine;

namespace Upform
{
    public class DesignerNavigation : MonoBehaviour
    {
        private const float PIXEL_TO_METER = 0.001f;
        private const float PIXEL_TO_ANGLE = 0.01f;
        private const float SCROLL_TO_METER = 0.1f;

        [Header("References")]
        [SerializeField] private Camera designerCamera;

        [Header("Keys")]
        [SerializeField] private KeyCode moveKey;

        [Header("Parameters")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float minZoomDistance = 2f;
        [SerializeField] private float maxZoomDistance = 20f;

        private bool _isMoving = false;
        private bool _canZoom = true;

        private Vector3 _mousePosition;
        private Vector3 _prevMousePosition;
        private Vector3 _moveStartPosition;
        private Vector3 _mouseMoveStartPosition;
        private Vector3 _mouseMoveOffset;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private float _initialCameraOrthographicSize;

        private void Awake()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
            _initialCameraOrthographicSize = designerCamera.orthographicSize;
        }

        private void Start()
        {
            _prevMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = 0;

            MoveCamera();

            ZoomCamera();

            _prevMousePosition = _mousePosition;
        }

        public void ResetTransform()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            designerCamera.orthographicSize = _initialCameraOrthographicSize;
        }

        private void MoveCamera()
        {
            if (!_isMoving && Input.GetKeyDown(moveKey))
            {
                _isMoving = true;
                _mouseMoveStartPosition = _mousePosition;
                _moveStartPosition = transform.localPosition;
            }

            if (_isMoving)
            {
                if (Input.GetKeyUp(moveKey))
                    _isMoving = false;

                _mouseMoveOffset = _mousePosition - _mouseMoveStartPosition;

                transform.localPosition = _moveStartPosition - transform.TransformDirection(PIXEL_TO_METER * moveSpeed * designerCamera.orthographicSize * _mouseMoveOffset);
            }
        }

        private void ZoomCamera()
        {
            if (_canZoom)
            {
                Zoom(Input.mouseScrollDelta.y);
            }
        }

        public void Zoom(float amount)
        {
            if (amount != 0f)
            {
                float zoomAmount = amount * zoomSpeed;

                designerCamera.orthographicSize = Mathf.Clamp(designerCamera.orthographicSize - zoomAmount, minZoomDistance, maxZoomDistance);
            }
        }
    }
}
