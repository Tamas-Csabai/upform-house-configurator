
using UnityEngine;
using Upform.Core;

namespace Upform.Designer
{
    public class Wall : EntityComponent
    {

        [Header("Resources")]
        [SerializeField] private WallSO wallSO;

        [Header("Child References")]
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private MeshFilter meshFilter;

        [Header("Points")]
        [SerializeField] private Point startPoint;
        [SerializeField] private Point endPoint;
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform bottomRightPoint;
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform topRightPoint;

        private Mesh _mesh;
        private float _length;
        private float _thickness;

        public Point StartPoint => startPoint;
        public Point EndPoint => endPoint;

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
            position.y = transform.position.y;

            transform.position = position;
        }

        public void SetActiveEndPoints(bool enabled)
        {
            startPoint.gameObject.SetActive(enabled);
            endPoint.gameObject.SetActive(enabled);
        }

        public void SetEndPointPosition(Vector3 endPosition)
        {
            endPosition.y = endPoint.transform.position.y;

            endPoint.transform.position = endPosition;

            Vector3 distance = endPoint.transform.position - startPoint.transform.position;

            Vector3 direction = distance.normalized;

            Vector3 cross = Vector3.Cross(direction, Vector3.up);

            float offsetZ = _thickness / 2f;

            topRightPoint.position = endPoint.transform.position + (cross * offsetZ);
            bottomRightPoint.position = endPoint.transform.position - (cross * offsetZ);

            topLeftPoint.position = startPoint.transform.position + (cross * offsetZ);
            bottomLeftPoint.position = startPoint.transform.position - (cross * offsetZ);

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

        public Vector3 GetClosestCenterPoint(Vector3 position)
        {
            Vector2 start = new Vector2(startPoint.transform.position.x, startPoint.transform.position.z);
            Vector2 end = new Vector2(endPoint.transform.position.x, endPoint.transform.position.z);
            Vector2 point = new Vector2(position.x, position.z);

            Vector2 pointOnLine = FindNearestPointOnLine(start, end, point);

            return new Vector3(pointOnLine.x, transform.position.y, pointOnLine.y);
        }

        public Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 end, Vector2 point)
        {
            //Get heading
            Vector2 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            Vector2 lhs = point - origin;
            float dotP = Vector2.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return origin + heading * dotP;
        }

    }
}
