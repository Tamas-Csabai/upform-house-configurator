
using UnityEngine;
using Upform.Core;
using Upform.Graphs;

namespace Upform.Designer
{
    public class Wall : EntityComponent
    {

        [SerializeField] private Edge edge;
        [SerializeField] private RectangleLine rectangleLine;
        [SerializeField] private MeshCollider meshCollider;

        private WallSO _wallSO;

        public Edge Edge => edge;

        public Vector3 Perpendicular => rectangleLine.Perpendicular;

        public float Thickness => rectangleLine.Thickness;

        public WallSO WallSO
        {
            get => _wallSO;
            set
            {
                _wallSO = value;

                gameObject.name = _wallSO.Name;
                rectangleLine.Thickness = _wallSO.Thickness;
            }
        }

        private void Awake()
        {
            edge.OnStartNodeChanged += StartNodeChanged;
            edge.OnEndNodeChanged += EndNodeChanged;
            edge.OnNodeMoved += NodeMoved;
        }

        private void NodeMoved(Node node)
        {
            if(node == edge.StartNode)
            {
                StartNodeChanged(node);
            }
            else
            {
                EndNodeChanged(node);
            }
        }

        private void OnDestroy()
        {
            if(edge != null)
            {
                edge.OnStartNodeChanged -= StartNodeChanged;
                edge.OnEndNodeChanged -= EndNodeChanged;
                edge.OnNodeMoved -= NodeMoved;
            }
        }

        private void StartNodeChanged(Node node)
        {
            rectangleLine.SetStartPoint(node.transform.position);

            UpdateMeshCollider();
        }

        private void EndNodeChanged(Node node)
        {
            rectangleLine.SetEndPoint(node.transform.position);

            UpdateMeshCollider();
        }

        private void UpdateMeshCollider()
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = rectangleLine.Mesh;
        }

        public void SetMeshColliderEnabled(bool enabled)
        {
            meshCollider.enabled = enabled;
        }

        public Vector3 GetClosestPosition(Vector3 position)
        {
            return rectangleLine.GetClosestPosition(position);
        }

    }
}
