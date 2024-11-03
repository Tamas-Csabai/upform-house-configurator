
using System.Collections.Generic;
using UnityEngine;
using Upform.Designer;

namespace Upform.Graphs
{
    public class Node : MonoBehaviour
    {

        [SerializeField] private Intersection intersection;

        private HashSet<Edge> _edges = new();

        public Intersection Intersection => intersection;
        public HashSet<Edge> Edges => _edges;

        public Edge GetEdge(Node otherNode)
        {
            foreach (Edge edge in _edges)
            {
                if(edge.StartNode == otherNode || edge.EndNode == otherNode)
                {
                    return edge;
                }
            }

            return null;
        }

        public void AddEdge(Edge edge)
        {
            _edges.Add(edge);
        }

        public void Move(Vector3 position)
        {
            transform.position = position;

            foreach(Edge edge in _edges)
            {
                edge.NodeMoved(this);
            }
        }
    }
}
