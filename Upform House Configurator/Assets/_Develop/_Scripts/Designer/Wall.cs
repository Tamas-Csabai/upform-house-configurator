using UnityEngine;
using Upform.Core;

namespace Upform.Designer
{
    public class Wall : EntityComponent
    {

        [SerializeField] private Edge edge;
        [SerializeField] private RectangleLine rectangleLine;

        public Vector3 GetClosestPosition(Vector3 position)
        {
            return rectangleLine.GetClosestPosition(position);
        }

    }
}
