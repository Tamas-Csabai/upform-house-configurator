
using System.Collections.Generic;
using UnityEngine;

namespace Upform.Designer
{
    public class Node : MonoBehaviour
    {

        private HashSet<Edge> _edges = new();

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
                edge.NodeMoved();
            }
        }
    }
}
