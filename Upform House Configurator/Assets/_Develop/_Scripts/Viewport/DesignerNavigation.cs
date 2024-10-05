using UnityEngine;
using Upform.Input;

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

        private Vector3 _pointerPosition;
        private Vector3 _prevPointerPosition;
        private Vector3 _moveStartPosition;
        private Vector3 _pointerMoveStartPosition;
        private Vector3 _pointerMoveOffset;

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
            _prevPointerPosition = InputManager.Actions.PointerPosition.Get();
        }

        private void Update()
        {
            _pointerPosition = InputManager.Actions.PointerPosition.Get();
            _pointerPosition.z = 0;

            MoveCamera();

            ZoomCamera();

            _prevPointerPosition = _pointerPosition;
        }

        public void ResetTransform()
        {
            transform.position = _initialPosition;
            transform.rotation = _initialRotation;
            designerCamera.orthographicSize = _initialCameraOrthographicSize;
        }

        private void MoveCamera()
        {
            if (!_isMoving && InputManager.Actions.CameraMove.GetDown())
            {
                _isMoving = true;
                _pointerMoveStartPosition = _pointerPosition;
                _moveStartPosition = transform.localPosition;
            }

            if (_isMoving)
            {
                if (InputManager.Actions.CameraMove.GetUp())
                    _isMoving = false;

                _pointerMoveOffset = _pointerPosition - _pointerMoveStartPosition;

                transform.localPosition = _moveStartPosition - transform.TransformDirection(PIXEL_TO_METER * moveSpeed * designerCamera.orthographicSize * _pointerMoveOffset);
            }
        }

        private void ZoomCamera()
        {
            if (_canZoom)
            {
                Zoom(InputManager.Actions.CameraZoomDelta.Get());
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
