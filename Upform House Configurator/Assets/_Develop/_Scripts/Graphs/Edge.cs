using UnityEngine;

namespace Upform.Graphs
{
    public class Edge : MonoBehaviour
    {

        private Node _startNode;
        private Node _endNode;

        public event System.Action<Node> OnStartNodeChanged;
        public event System.Action<Node> OnEndNodeChanged;
        public event System.Action<Node> OnNodeMoved;

        public Node StartNode
        {
            get => _startNode;
            set
            {
                _startNode = value;
                OnStartNodeChanged?.Invoke(value);
            }
        }

        public Node EndNode
        {
            get => _endNode;
            set
            {
                _endNode = value;
                OnEndNodeChanged?.Invoke(value);
            }
        }

        public void NodeMoved(Node node)
        {
            OnNodeMoved?.Invoke(node);
        }

    }
}
