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

        public void StartMove()
        {
            SetCollider(false);

            foreach (Edge edge in node.Edges)
            {
                Wall wall = edge.Wall;

                wall.StartMove();
                wall.SetMeshColliderEnabled(false);
            }
        }

        public void Move(Vector3 position)
        {
            node.Move(position);
        }

        public void StopMove()
        {
            SetCollider(true);

            foreach (Edge edge in node.Edges)
            {
                Wall wall = edge.Wall;

                wall.StopMove();
                wall.SetMeshColliderEnabled(true);
            }
        }

        public void SetCollider(bool enabled)
        {
            collider.enabled = enabled;
        }

    }
}
