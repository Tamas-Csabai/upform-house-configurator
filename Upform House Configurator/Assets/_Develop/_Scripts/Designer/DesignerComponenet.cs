using UnityEngine;
using Upform.Graphs;

namespace Upform.Designer
{
    public abstract class DesignerComponenet : MonoBehaviour
    {

        protected float _verticalOffset;
        protected Graph _graph;
        protected Grid _grid;

        public void Initialize(float verticalOffset, Graph graph, Grid grid)
        {
            _verticalOffset = verticalOffset;
            _graph = graph;
            _grid = grid;
        }

    }
}
