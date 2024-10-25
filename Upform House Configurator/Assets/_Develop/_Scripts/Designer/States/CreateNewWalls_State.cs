
using UnityEngine;
using Upform.Interaction;
using Upform.Selection;
using Upform.States;

namespace Upform.Designer
{
    public class CreateNewWalls_State : StateBase
    {

        [SerializeField] private StateSO hasSelectionStateSO;
        [SerializeField] private Graph graph;
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private GameObject pointVisualObjectPrefab;
        [SerializeField] private GameObject confirmPointVisualPrefab;
        [SerializeField] private RectangleLine newWallPrefab;
        [SerializeField] private Transform wallsParent;

        private Vector3 _wallStartPoint;
        private bool _isStartPointPlaced;

        private RectangleLine _currentHoveredWall;
        private RectangleLine _newWall;
        private RectangleLine _prevWall;

        private GameObject _pointVisualObject;
        private GameObject _confirmPointVisual;

        private Point _currentSnapPoint;
        private Point _prevSnapPoint;

        private Node _selectedNode;
        private Node _prevSelectedNode;

        public override void OnEntering()
        {
            /*
            if(_pointVisualObject == null)
            {
                _pointVisualObject = Instantiate(pointVisualObjectPrefab);
                _pointVisualObject.transform.SetParent(transform, true);
                _pointVisualObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (_confirmPointVisual == null)
            {
                _confirmPointVisual = Instantiate(confirmPointVisualPrefab);
                _confirmPointVisual.transform.SetParent(transform, true);
                _confirmPointVisual.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _pointVisualObject.SetActive(true);

            SelectionManager.ClearSelection();

            SetCurrentSnapPoint(null);
            _prevSnapPoint = null;

            _currentHoveredWall = null;
            _prevWall = null;
            _newWall = null;

            _wallStartPoint = Vector3.zero;

            _selectedNode = null;
            _prevSelectedNode = null;

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;
            */
            stateMachine.EnterDefaultState();
        }

        public override void OnExiting()
        {
            /*
            _pointVisualObject.SetActive(false);

            SetCurrentSnapPoint(null);
            _prevSnapPoint = null;

            InteractionManager.OnInteract -= Interact;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnHoverEnter -= HoverEnter;
            InteractionManager.OnHoverExit -= HoverExit;
            */
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            /*
            if(interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                if(interactionHit.Interactable.TryGetComponent(out RectangleLine wall))
                {
                    if (wall != _newWall)
                    {
                        _currentHoveredWall = wall;
                    }
                }
            }
            else if (interactionHit.Interactable.HasLayer(InteractionLayer.Intersection))
            {
                if (interactionHit.Interactable.TryGetComponent(out Point point))
                {
                    if(_newWall == null)
                    {
                        SetCurrentSnapPoint(point);
                    }
                }
            }*/
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            /*
            if (interactionHit.Interactable == null)
            {
                _currentHoveredWall = null;
                return;
            }

            if(_currentSnapPoint != null)
            {
                SetCurrentSnapPoint(_prevSnapPoint);
            }

            if (interactionHit.Interactable.TryGetComponent(out RectangleLine wall))
            {
                if (wall != _newWall)
                {
                    _currentHoveredWall = wall;
                }
            }
            else
            {
                _currentHoveredWall = null;
            }*/
        }

        private void Hovering(InteractionHit interactionHit)
        {
            /*
            if (_newWall != null)
            {
                if(_currentSnapPoint != null)
                {
                    _newWall.SetEndPointPosition(_currentSnapPoint.transform.position);
                }
                else if (_currentHoveredWall != null)
                {
                    Vector3 closestCenterPoint = _currentHoveredWall.GetClosestCenterPoint(interactionHit.Point);
                    _newWall.SetEndPointPosition(closestCenterPoint);
                }
                else
                {
                    _newWall.SetEndPointPosition(interactionHit.Point);
                }

                _pointVisualObject.transform.position = _newWall.EndPoint.transform.position;
            }
            else
            {
                _pointVisualObject.transform.position = interactionHit.Point;
            }
            */
        }

        private void Interact(InteractionHit interactionHit)
        {
            /*
            if (!_isStartPointPlaced)
            {
                _wallStartPoint = interactionHit.Point;

                _newWall = CreateNewWall();

                _isStartPointPlaced = true;
            }
            else
            {
                Vector3 hitPoint = new Vector3(interactionHit.Point.x, wallsParent.position.y, interactionHit.Point.z);

                _newWall.SetEndPointPosition(interactionHit.Point);
                _newWall.UpdateMeshCollider();
                _newWall.SetActiveEndPoints(true);


                _prevWall = _newWall;

                _wallStartPoint = interactionHit.Point;

                _newWall = CreateNewWall();
            }
            */
        }

        private void SetCurrentSnapPoint(Point point)
        {
            /*
            if(_currentSnapPoint != null)
            {
                //_currentSnapPoint.Interactable.OnInteract -= Confirm;
            }

            _prevSnapPoint = _currentSnapPoint;
            _currentSnapPoint = point;

            if (_currentSnapPoint != null)
            {
                //_currentSnapPoint.Interactable.OnInteract += Confirm;
                _confirmPointVisual.transform.position = _currentSnapPoint.transform.position;
                _confirmPointVisual.gameObject.SetActive(true);
            }
            else
            {
                _confirmPointVisual.gameObject.SetActive(false);
            }*/
        }

        private void Confirm()
        {
            /*
            if (!_isStartPointPlaced)
            {
                return;
            }

            if (_prevWall != null)
            {
                Destroy(_newWall.gameObject);

                Selectable newWallSelectable = _prevWall.GetComponent<Selectable>();

                newWallSelectable.SelectOnly();
            }
            else
            {
                _newWall.UpdateMeshCollider();
                _newWall.SetActiveEndPoints(true);

                Selectable newWallSelectable = _newWall.GetComponent<Selectable>();

                newWallSelectable.SelectOnly();
            }

            SetCurrentSnapPoint(null);

            Exit(hasSelectionStateSO);*/
        }

        private RectangleLine CreateNewWall()
        {
            RectangleLine newWall = Instantiate(newWallPrefab);

            newWall.transform.SetParent(wallsParent, true);
            newWall.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            newWall.SetActiveEndPoints(false);
            newWall.Move(_wallStartPoint);

            return newWall;
        }

    }
}
