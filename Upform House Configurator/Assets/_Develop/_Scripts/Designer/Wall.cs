using UnityEngine;
using Upform.Interaction;

namespace Upform
{
    public class Wall : MonoBehaviour
    {

        [Header("Resources")]
        [SerializeField] private WallSO wallSO;
        [SerializeField] private Material material;

        [Header("Child References")]
        [SerializeField] private Interactable interactable;
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform bottomRightPoint;
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform topRightPoint;

        private Mesh _mesh;
        private MeshCollider _meshCollider;
        private RenderParams _renderParams;
        private Matrix4x4 _worldTransformMatrix;
        private float _length;
        private float _thickness;

        public float Length => _length;

        public float Thickness
        {
            get => _thickness;

            set
            {
                _thickness = value;

                float offsetZ = _thickness / 2f;

                Vector3 bottomLeft = bottomLeftPoint.localPosition;
                bottomLeft.z = -offsetZ;
                bottomLeftPoint.transform.localPosition = bottomLeft;

                Vector3 bottomRight = bottomRightPoint.localPosition;
                bottomRight.z = -offsetZ;
                bottomRightPoint.transform.localPosition = bottomRight;

                Vector3 topLeft = topLeftPoint.localPosition;
                topLeft.z = offsetZ;
                topLeftPoint.localPosition = topLeft;

                Vector3 topRight = topRightPoint.localPosition;
                topRight.z = offsetZ;
                topRightPoint.localPosition = topRight;

                RecalculateMeshTransform();
            }
        }

        private void Awake()
        {
            _mesh = new();

            _meshCollider = interactable.Collider as MeshCollider;

            _renderParams = new RenderParams(material);

            Thickness = wallSO.Thickness;

            _length = Mathf.Abs(bottomLeftPoint.localPosition.x - bottomRightPoint.localPosition.x);

            RecalculateMeshTransform();
        }

        private void LateUpdate()
        {
            RenderMesh();
        }

        public void Move(Vector3 position)
        {
            transform.position = position;

            RecalculateMeshTransform();
        }

        private void RecalculateMeshTransform()
        {
            MeshBuilder.CreateQuadForMesh(ref _mesh, bottomLeftPoint.localPosition, bottomRightPoint.localPosition, topLeftPoint.localPosition, topRightPoint.localPosition);

            _worldTransformMatrix = Matrix4x4.Translate(transform.position);

            _meshCollider.sharedMesh = null;
            _meshCollider.sharedMesh = _mesh;
        }

        private void RenderMesh()
        {
            Graphics.RenderMesh(_renderParams, _mesh, 0, _worldTransformMatrix);
        }

    }
}
