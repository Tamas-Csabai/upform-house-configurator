using UnityEngine;
using Upform.Core;
using Upform.Graphs;

namespace Upform.Designer
{
    public class Intersection : EntityComponent
    {

        [SerializeField] private Node node;
        [SerializeField] private Point point;
        [SerializeField] private new Collider collider;

        private WallSO _wallSO;

        public Node Node => node;

        public WallSO WallSO
        {
            get => _wallSO;
            set
            {
                _wallSO = value;

                gameObject.name = _wallSO.Name + " Intersection";
                point.SetSize(_wallSO.Thickness);
            }
        }

        public void Move(Vector3 position)
        {
            node.Move(position);
        }

        public void SetCollider(bool enabled)
        {
            collider.enabled = enabled;
        }

        public void SetWallColliders(bool enabled)
        {
            foreach(Edge edge in node.Edges)
            {
                Wall wall = edge.GetComponent<Wall>();

                wall.SetMeshColliderEnabled(enabled);
            }
        }

    }
}
