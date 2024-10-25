using UnityEngine;

namespace Upform.Designer
{
    public class Edge : MonoBehaviour
    {

        private Node _startNode;
        private Node _endNode;

        public event System.Action OnStartNodeChanged;
        public event System.Action OnEndNodeChanged;
        public event System.Action OnNodeMoved;

        public Node StartNode
        {
            get => _startNode;
            set
            {
                _startNode = value;
                OnStartNodeChanged?.Invoke();
            }
        }

        public Node EndNode
        {
            get => _endNode;
            set
            {
                _endNode = value;
                OnEndNodeChanged?.Invoke();
            }
        }

        public void NodeMoved()
        {
            OnNodeMoved?.Invoke();
        }

    }
}
