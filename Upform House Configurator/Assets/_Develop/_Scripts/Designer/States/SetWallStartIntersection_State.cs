
using UnityEngine;
using Upform.Interaction;
using Upform.States;

namespace Upform.Designer
{
    public class SetWallStartIntersection_State : StateBase
    {

        [SerializeField] private StateSO setWallEndIntersection;
        [SerializeField] private Graph graph;
        [SerializeField] private GameObject pointVisualObjectPrefab;
        [SerializeField] private Intersection intersectionPrefab;

        private Vector3 _newIntersectionPosition;
        private bool _hasSnapPoint;
        private GameObject _pointVisualObject;
        private Intersection _startIntersection;

        public override void OnEntering()
        {
            if (_pointVisualObject == null)
            {
                _pointVisualObject = Instantiate(pointVisualObjectPrefab);
                _pointVisualObject.transform.SetParent(transform, true);
                _pointVisualObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _newIntersectionPosition = Vector3.zero;
            _hasSnapPoint = false;
            _pointVisualObject.SetActive(true);
            _startIntersection = null;

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;
        }

        public override void OnExiting()
        {
            InteractionManager.OnInteract -= Interact;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnHoverEnter -= HoverEnter;
            InteractionManager.OnHoverExit -= HoverExit;

            _pointVisualObject.SetActive(false);
        }

        private void Interact(InteractionHit interactionHit)
        {
            if (_hasSnapPoint)
            {

            }
            else
            {

            }

            Exit(setWallEndIntersection);
        }

        private void Hovering(InteractionHit interactionHit)
        {
            if (!_hasSnapPoint)
            {
                _pointVisualObject.transform.position = interactionHit.Point;

                _newIntersectionPosition = interactionHit.Point;
            }
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            if (interactionHit.Interactable.HasLayer(InteractionLayer.Intersection))
            {
                _hasSnapPoint = true;

                Intersection intersection = interactionHit.Interactable.GetComponent<Intersection>();

                _startIntersection = intersection;

                _pointVisualObject.transform.position = intersection.transform.position;
            }
            else if (interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                _hasSnapPoint = true;

                Wall wall = interactionHit.Interactable.GetComponent<Wall>();

                _newIntersectionPosition = wall.GetClosestPosition(interactionHit.Point);

                _pointVisualObject.transform.position = _newIntersectionPosition;
            }
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            _hasSnapPoint = false;

            _startIntersection = null;
        }
    }
}
