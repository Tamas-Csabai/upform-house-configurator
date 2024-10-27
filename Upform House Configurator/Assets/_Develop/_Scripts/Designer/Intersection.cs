using UnityEngine;
using Upform.Core;
using Upform.Graphs;

namespace Upform.Designer
{
    public class Intersection : EntityComponent
    {

        [SerializeField] private Node node;
        [SerializeField] private Point point;

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

    }
}
