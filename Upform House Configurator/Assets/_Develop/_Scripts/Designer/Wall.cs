
using UnityEngine;

namespace Upform.Designer
{
    public class Wall : MonoBehaviour
    {

        [Header("Resources")]
        [SerializeField] private WallSO wallSO;

        [Header("Child References")]
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private MeshFilter meshFilter;

        [Header("Points")]
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform bottomRightPoint;
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform topRightPoint;

        private Mesh _mesh;
        private float _length;
        private float _thickness;

        public float Length
        {
            get => _length;
            set => _length = value;
        }

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

                RecalculateMesh();

                UpdateMeshCollider();
            }
        }

        private void Awake()
        {
            _mesh = new();

            Thickness = wallSO.Thickness;
            Length = Mathf.Abs(bottomLeftPoint.localPosition.x - bottomRightPoint.localPosition.x);

            RecalculateMesh();

            UpdateMeshCollider();
        }

        public void Move(Vector3 position)
        {
            transform.position = position;

            RecalculateMesh();

            UpdateMeshCollider();
        }

        public void SetEndPoint(Vector3 endPoint)
        {
            float offsetZ = _thickness / 2f;

            topRightPoint.position = endPoint;

            bottomRightPoint.position = endPoint;

            RecalculateMesh();
        }

        public void RecalculateMesh()
        {
            MeshBuilder.CreateQuadForMesh(ref _mesh, bottomLeftPoint.localPosition, bottomRightPoint.localPosition, topLeftPoint.localPosition, topRightPoint.localPosition);

            meshFilter.mesh = _mesh;
        }

        public void UpdateMeshCollider()
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = _mesh;
        }

    }
}
