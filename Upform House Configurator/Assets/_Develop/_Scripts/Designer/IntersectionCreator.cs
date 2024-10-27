
using UnityEngine;
using Upform.Graphs;
using Upform.Interaction;

namespace Upform.Designer
{
    public class IntersectionCreator : MonoBehaviour
    {

        [SerializeField] private float verticalOffset = 0.1f;
        [SerializeField] private GameObject pointVisualObjectPrefab;
        [SerializeField] private GameObject confirmVisualObjectPrefab;
        [SerializeField] private Graph graph;

        private Vector3 _newIntersectionPosition;
        private bool _hasSnapPoint;
        private GameObject _pointVisualObject;
        private GameObject _confirmVisualObject;
        private Intersection _currentIntersection;
        private Wall _currentHoveredWall;

        public event System.Action<Intersection> OnNewIntersection;
        public event System.Action<Intersection> OnIntersectionSelected;
        public event System.Action<Intersection, Wall> OnNewIntersectionOnWall;

        public void StartInteraction()
        {
            if (_pointVisualObject == null)
            {
                _pointVisualObject = Instantiate(pointVisualObjectPrefab);
                _pointVisualObject.transform.SetParent(transform, true);
                _pointVisualObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (_confirmVisualObject == null)
            {
                _confirmVisualObject = Instantiate(confirmVisualObjectPrefab);
                _confirmVisualObject.transform.SetParent(transform, true);
                _confirmVisualObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _newIntersectionPosition = Vector3.zero;
            _hasSnapPoint = false;
            _pointVisualObject.SetActive(true);
            _confirmVisualObject.SetActive(false);

            _currentHoveredWall = null;
            _currentIntersection = null;

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;
        }

        public void StopInteraction()
        {
            InteractionManager.OnInteract -= Interact;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnHoverEnter -= HoverEnter;
            InteractionManager.OnHoverExit -= HoverExit;

            _pointVisualObject.SetActive(false);
            _confirmVisualObject.SetActive(false);
        }

        private void Interact(InteractionHit interactionHit)
        {
            if (_currentIntersection == null)
            {
                Node newNode = graph.AddNewNode(_newIntersectionPosition);

                _currentIntersection = newNode.GetComponent<Intersection>();

                if (_currentHoveredWall != null)
                {
                    graph.InsertNode(newNode, _currentHoveredWall.Edge);

                    OnNewIntersectionOnWall?.Invoke(_currentIntersection, _currentHoveredWall);
                }
                else
                {
                    OnNewIntersection?.Invoke(_currentIntersection);
                }
            }
            else
            {
                OnIntersectionSelected?.Invoke(_currentIntersection);
            }
        }

        private void Hovering(InteractionHit interactionHit)
        {
            if (!_hasSnapPoint)
            {
                _newIntersectionPosition = interactionHit.Point + (verticalOffset * Vector3.up);
            }
            else
            {
                if(_currentHoveredWall != null)
                {
                    _newIntersectionPosition = _currentHoveredWall.GetClosestPosition(interactionHit.Point);

                    _confirmVisualObject.transform.position = _newIntersectionPosition;
                }
            }

            _pointVisualObject.transform.position = _newIntersectionPosition;
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            if (interactionHit.Interactable.HasLayer(InteractionLayer.Intersection))
            {
                _hasSnapPoint = true;

                Intersection intersection = interactionHit.Interactable.GetComponent<Intersection>();

                _currentIntersection = intersection;

                _newIntersectionPosition = intersection.transform.position;

                _confirmVisualObject.transform.position = _newIntersectionPosition;
                _confirmVisualObject.SetActive(true);
            }
            else if (interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                _hasSnapPoint = true;

                _currentHoveredWall = interactionHit.Interactable.GetComponent<Wall>();

                _confirmVisualObject.SetActive(true);
            }
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            _hasSnapPoint = false;

            _currentIntersection = null;

            _currentHoveredWall = null;

            _confirmVisualObject.SetActive(false);
        }

    }
}
