
using UnityEngine;
using Upform.Designer;
using Upform.Interaction;
using Upform.Selection;
using Upform.States;

namespace Upform.Core
{
    public class CreateNewWallState : StateBase
    {

        [SerializeField] private StateSO hasSelectionStateSO;
        [SerializeField] private GameObject pointVisualObjectPrefab;
        [SerializeField] private GameObject confirmPointVisualPrefab;
        [SerializeField] private Wall newWallPrefab;
        [SerializeField] private Transform wallsParent;

        private bool _isStartPointPlaced = false;
        private Vector3 _wallStartPoint;
        private Wall _currentHoveredWall;

        private GameObject _pointVisualObject;
        private GameObject _confirmPointVisual;
        private Point _currentSnapPoint;
        private Point _prevSnapPoint;
        private Wall _newWall;
        private Wall _prevWall;

        public override void OnEntering()
        {
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
            _currentHoveredWall = null;
            _prevWall = null;
            _newWall = null;
            _isStartPointPlaced = false;
            _wallStartPoint = Vector3.zero;

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;
        }

        public override void Evaluate()
        {

        }

        public override void OnExiting()
        {
            _pointVisualObject.SetActive(false);

            InteractionManager.OnInteract -= Interact;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnHoverEnter -= HoverEnter;
            InteractionManager.OnHoverExit -= HoverExit;
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            if(interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                if(interactionHit.Interactable.TryGetComponent(out Wall wall))
                {
                    if (wall != _newWall)
                    {
                        _currentHoveredWall = wall;
                    }
                }
            }
            else if (interactionHit.Interactable.HasLayer(InteractionLayer.Point))
            {
                if (interactionHit.Interactable.TryGetComponent(out Point point))
                {
                    if(_newWall == null || _newWall.Entity != point.Entity)
                    {
                        SetCurrentSnapPoint(point);
                    }
                }
            }
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            if (interactionHit.Interactable == null)
            {
                _currentHoveredWall = null;
                return;
            }

            if(_currentSnapPoint != null)
            {
                SetCurrentSnapPoint(_prevSnapPoint);
            }

            if (interactionHit.Interactable.TryGetComponent(out Wall wall))
            {
                if (wall != _newWall)
                {
                    _currentHoveredWall = wall;
                }
            }
            else
            {
                _currentHoveredWall = null;
            }
        }

        private void Hovering(InteractionHit interactionHit)
        {
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
        }

        private void Interact(InteractionHit interactionHit)
        {
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

                SetCurrentSnapPoint(_newWall.EndPoint);

                _prevWall = _newWall;

                _wallStartPoint = interactionHit.Point;

                _newWall = CreateNewWall();
            }
        }

        private void SetCurrentSnapPoint(Point point)
        {
            if(_currentSnapPoint != null)
            {
                _currentSnapPoint.Interactable.OnInteract -= Confirm;
            }

            _prevSnapPoint = _currentSnapPoint;
            _currentSnapPoint = point;

            if (_currentSnapPoint != null)
            {
                _currentSnapPoint.Interactable.OnInteract += Confirm;
                _confirmPointVisual.transform.position = _currentSnapPoint.transform.position;
                _confirmPointVisual.gameObject.SetActive(true);
            }
            else
            {
                _confirmPointVisual.gameObject.SetActive(false);
            }
        }

        private void Confirm()
        {
            if (!_isStartPointPlaced)
            {
                return;
            }

            if (_prevWall != null && _currentSnapPoint == _prevWall.EndPoint)
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

            Exit(hasSelectionStateSO);
        }

        private Wall CreateNewWall()
        {
            Wall newWall = Instantiate(newWallPrefab);

            newWall.transform.SetParent(wallsParent, true);
            newWall.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            newWall.SetActiveEndPoints(false);
            newWall.Move(_wallStartPoint);

            return newWall;
        }

    }
}
