
using UnityEngine;
using Upform.Interaction;

namespace Upform.Designer
{
    public class IntersectionMover : DesignerComponenet
    {

        private Intersection _intersection;

        public void StartInteraction(Intersection intersection)
        {
            _intersection = intersection;

            _intersection.SetWallColliders(false);
            _intersection.SetCollider(false);

            InteractionManager.OnHovering += Hovering;
        }

        private void Hovering(InteractionHit interactionHit)
        {
            Vector3 interactionPoint = _grid.WorldToCellOnPlane(interactionHit.Point);

            _intersection.Move(interactionPoint);
        }

        public void StopInteraction()
        {
            if(_intersection != null)
            {
                _intersection.SetWallColliders(true);
                _intersection.SetCollider(true);
            }

            _intersection = null;

            InteractionManager.OnHovering -= Hovering;
        }

    }
}
