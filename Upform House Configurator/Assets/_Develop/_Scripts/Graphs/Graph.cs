using System.Collections.Generic;
using UnityEngine;

namespace Upform.Graphs
{
    public class Graph : MonoBehaviour 
    {

        [SerializeField] private Node nodePrefab;
        [SerializeField] private Edge edgePrefab;
        [SerializeField] private Transform nodesParent;
        [SerializeField] private Transform edgesParent;

        private HashSet<Node> _nodes = new();
        private HashSet<Edge> _edges = new();

        private Node CreateNewNode()
        {
            Node newNode = Instantiate(nodePrefab);
            newNode.transform.SetParent(nodesParent);
            return newNode;
        }

        private Edge CreateNewEdge(Node startNode, Node endNode)
        {
            Edge newEdge = Instantiate(edgePrefab);
            newEdge.transform.SetParent(edgesParent);
            newEdge.StartNode = startNode;
            newEdge.EndNode = endNode;
            return newEdge;
        }

        public Node AddNewNode(Vector3 position)
        {
            Node newNode = CreateNewNode();
            newNode.Move(position);
            _nodes.Add(newNode);
            return newNode;
        }

        public Edge ConnectNodes(Node startNode, Node endNode)
        {
            Edge edge = startNode.GetEdge(endNode);

            if (edge == null)
            {
                Edge newEdge = CreateNewEdge(startNode, endNode);
                startNode.AddEdge(newEdge);
                endNode.AddEdge(newEdge);
                _edges.Add(newEdge);
                return newEdge; 
            }

            return edge;
        }

        public Edge InsertNode(Node node, Edge edge)
        {
            Edge newEdge = ConnectNodes(edge.StartNode, node);
            _edges.Add(newEdge);

            edge.StartNode = node;
            node.AddEdge(edge);

            return newEdge;
        }

    }
}
