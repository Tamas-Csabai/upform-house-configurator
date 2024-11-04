
using UnityEngine;
using Upform.Graphs;
using Upform.Interaction;

namespace Upform.Designer
{
    public class IntersectionCreator : DesignerComponenet
    {

        [SerializeField] private Point newIntersectionVisualPointPrefab;
        [SerializeField] private Point confirmVisualPointPrefab;

        private Vector3 _newIntersectionPosition;
        private bool _hasSnapPoint;
        private Point _newIntersectionVisualPoint;
        private Point _confirmVisualPoint;
        private Intersection _currentIntersection;
        private Intersection _prevIntersection;
        private Wall _currentHoveredWall;
        private WallSO _wallSO;

        public event System.Action<Intersection> OnNewIntersection;
        public event System.Action<Intersection> OnIntersectionSelected;
        public event System.Action<Intersection, Wall> OnNewIntersectionOnWall;

        public void StartInteraction(WallSO wallSO)
        {
            if (_newIntersectionVisualPoint == null)
            {
                _newIntersectionVisualPoint = Instantiate(newIntersectionVisualPointPrefab);
                _newIntersectionVisualPoint.transform.SetParent(transform, true);
                _newIntersectionVisualPoint.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (_confirmVisualPoint == null)
            {
                _confirmVisualPoint = Instantiate(confirmVisualPointPrefab);
                _confirmVisualPoint.transform.SetParent(transform, true);
                _confirmVisualPoint.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _wallSO = wallSO;

            _newIntersectionPosition = Vector3.zero;
            _hasSnapPoint = false;

            _newIntersectionVisualPoint.gameObject.SetActive(true);
            _newIntersectionVisualPoint.SetSize(_wallSO.Thickness);

            _confirmVisualPoint.gameObject.SetActive(false);
            _confirmVisualPoint.SetSize(_wallSO.Thickness);

            _currentHoveredWall = null;
            _currentIntersection = null;
            _prevIntersection = null;

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

            _newIntersectionVisualPoint.gameObject.SetActive(false);
            _confirmVisualPoint.gameObject.SetActive(false);
            _designerHandler.SetAngleVisualActive(false);
        }

        private void Interact(InteractionHit interactionHit)
        {
            if (_currentIntersection == null)
            {
                Node newNode = _designerHandler.Graph.AddNewNode(_newIntersectionPosition);

                _currentIntersection = newNode.GetComponent<Intersection>();
                _currentIntersection.WallSO = _wallSO;

                if (_currentHoveredWall != null)
                {
                    Edge newEdge = _designerHandler.Graph.InsertNode(newNode, _currentHoveredWall.Edge);

                    Wall newWall = newEdge.GetComponent<Wall>();
                    newWall.WallSO = _currentHoveredWall.WallSO;

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

            _prevIntersection = _currentIntersection;
            _currentIntersection = null;
        }

        private void Hovering(InteractionHit interactionHit)
        {
            Vector3 interactionPoint = _designerHandler.GridSnap.WorldToCellCenterOnPlane(interactionHit.Point);

            if (!_hasSnapPoint)
            {
                Vector3 angledPosition = interactionPoint;

                if (_prevIntersection != null)
                {
                    angledPosition = _designerHandler.AngleSnap.WorldToAngleOnPlane(_prevIntersection.transform.position, interactionPoint);

                    _designerHandler.SetAngleVisualActive(true);
                    _designerHandler.SetAngleVisual(_prevIntersection.transform.position, _prevIntersection.transform.position + Vector3.right, angledPosition);
                }

                _newIntersectionPosition = angledPosition + (_designerHandler.VerticalOffset * Vector3.up);
            }
            else
            {
                if(_currentHoveredWall != null)
                {
                    _newIntersectionPosition = _currentHoveredWall.GetClosestPosition(interactionPoint);

                    _confirmVisualPoint.transform.position = _newIntersectionPosition;

                    if (_prevIntersection != null)
                    {
                        _designerHandler.SetAngleVisualActive(true);
                        _designerHandler.SetAngleVisual(_prevIntersection.transform.position, _prevIntersection.transform.position + Vector3.right, _newIntersectionPosition);
                    }
                }
            }

            _newIntersectionVisualPoint.transform.position = _newIntersectionPosition;
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            if (interactionHit.Interactable.HasLayer(InteractionLayer.Intersection))
            {
                _hasSnapPoint = true;

                Intersection intersection = interactionHit.Interactable.GetComponent<Intersection>();

                _currentIntersection = intersection;

                _newIntersectionPosition = intersection.transform.position;

                _confirmVisualPoint.transform.position = _newIntersectionPosition;
                _confirmVisualPoint.gameObject.SetActive(true);
            }
            else if (interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                _hasSnapPoint = true;

                _currentHoveredWall = interactionHit.Interactable.GetComponent<Wall>();

                _confirmVisualPoint.gameObject.SetActive(true);
            }
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            _hasSnapPoint = false;

            _currentIntersection = null;

            _currentHoveredWall = null;

            _confirmVisualPoint.gameObject.SetActive(false);
        }

    }
}
