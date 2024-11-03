using UnityEngine;
using Upform.Graphs;

namespace Upform.Designer
{
    public abstract class DesignerComponenet : MonoBehaviour
    {

        protected float _verticalOffset;
        protected Graph _graph;
        protected CellGrid _grid;
        protected Angles _angles;

        public void Initialize(float verticalOffset, Graph graph, CellGrid grid, Angles angles)
        {
            _verticalOffset = verticalOffset;
            _graph = graph;
            _grid = grid;
            _angles = angles;
        }

    }
}
