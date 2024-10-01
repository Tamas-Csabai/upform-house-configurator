using UnityEngine;

namespace Upform
{
    public class Wall : MonoBehaviour
    {

        [SerializeField] private WallSO wallSO;
        [SerializeField] private Material material;
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform bottomRightPoint;
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform topRightPoint;

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
            }
        }

        private void Awake()
        {
            Thickness = wallSO.Thickness;

            _length = Mathf.Abs(bottomLeftPoint.localPosition.x - bottomRightPoint.localPosition.x);
        }

        private void LateUpdate()
        {
            RenderMesh();
        }

        private void RenderMesh()
        {
            Mesh quad = MeshBuilder.CreateQuad(bottomLeftPoint.localPosition, bottomRightPoint.localPosition, topLeftPoint.localPosition, topRightPoint.localPosition);

            RenderParams rp = new RenderParams(material);

            Graphics.RenderMesh(rp, quad, 0, Matrix4x4.Translate(transform.position));
        }

    }
}
