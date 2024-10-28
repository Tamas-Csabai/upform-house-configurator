
using UnityEngine;

namespace Upform.Designer
{
    public class RectangleLine : MonoBehaviour
    {

        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform bottomRightPoint;
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform topRightPoint;

        private Mesh _mesh;
        private float _length;
        private float _thickness;

        public Mesh Mesh => _mesh;

        public Vector3 Perpendicular
        {
            get
            {
                Vector3 cross = Vector3.Cross((endPoint.transform.position - startPoint.transform.position).normalized, Vector3.up);
                return Vector3.ProjectOnPlane(cross, Vector3.up);
            }
        }

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

                UpdateEndPoints();

                RecalculateMesh();
            }
        }

        private void Awake()
        {
            _mesh = MeshBuilder.CreateNewQuad(bottomLeftPoint.localPosition, bottomRightPoint.localPosition, topLeftPoint.localPosition, topRightPoint.localPosition);

            _length = Mathf.Abs(bottomLeftPoint.localPosition.x - bottomRightPoint.localPosition.x);

            _thickness = (bottomLeftPoint.position - topLeftPoint.position).magnitude;

            RecalculateMesh();
        }

        public void SetStartPoint(Vector3 position)
        {
            startPoint.transform.position = position;

            UpdateEndPoints();

            RecalculateMesh();
        }

        public void SetEndPoint(Vector3 position)
        {
            endPoint.transform.position = position;

            UpdateEndPoints();

            RecalculateMesh();
        }

        public void RecalculateMesh()
        {
            MeshBuilder.SetQuadVertices(ref _mesh, bottomLeftPoint.localPosition, bottomRightPoint.localPosition, topLeftPoint.localPosition, topRightPoint.localPosition);

            meshFilter.mesh = _mesh;
        }

        public Vector3 GetClosestPosition(Vector3 position)
        {
            return Utils.FindClosestPositionOnSection(startPoint.position, endPoint.position, position);
        }

        private void UpdateEndPoints()
        {
            Vector3 distance = endPoint.transform.position - startPoint.transform.position;

            Vector3 direction = distance.normalized;

            Vector3 cross = Vector3.Cross(direction, Vector3.up);

            float offsetZ = _thickness / 2f;

            topLeftPoint.position = startPoint.transform.position + (cross * offsetZ);
            bottomLeftPoint.position = startPoint.transform.position - (cross * offsetZ);

            topRightPoint.position = endPoint.transform.position + (cross * offsetZ);
            bottomRightPoint.position = endPoint.transform.position - (cross * offsetZ);
        }

    }
}
