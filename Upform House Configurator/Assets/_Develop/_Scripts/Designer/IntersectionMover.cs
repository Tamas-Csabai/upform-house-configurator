
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

            _intersection.StartMove();
            
            InteractionManager.OnHovering += Hovering;
        }

        private void Hovering(InteractionHit interactionHit)
        {
            Vector3 interactionPoint = _designerHandler.GridSnap.WorldToCellCenterOnPlane(interactionHit.Point);

            _intersection.Move(interactionPoint);
        }

        public void StopInteraction()
        {
            if(_intersection != null)
            {
                _intersection.StopMove();
            }

            _intersection = null;

            InteractionManager.OnHovering -= Hovering;
        }

    }
}
