
using System;
using System.Collections;
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
        [SerializeField] private GameObject confirmPointObjectPrefab;
        [SerializeField] private Wall newWallPrefab;
        [SerializeField] private Transform wallsParent;

        private bool _isStartPointPlaced = false;
        private Vector3 _wallStartPoint;
        private Wall _currentHoveredWall;

        private GameObject _pointVisualObject;
        private Interactable _confirmPointInteractable;
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

            if (_confirmPointInteractable == null)
            {
                _confirmPointInteractable = Instantiate(confirmPointObjectPrefab).GetComponent<Interactable>();
                _confirmPointInteractable.transform.SetParent(transform, true);
                _confirmPointInteractable.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                _confirmPointInteractable.OnInteract += Confirm;
            }

            _pointVisualObject.SetActive(true);
            _confirmPointInteractable.gameObject.SetActive(false);

            SelectionManager.ClearSelection();

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
            if(interactionHit.Interactable.TryGetComponent(out Wall wall))
            {
                if(wall != _newWall)
                {
                    _currentHoveredWall = wall;
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
            _pointVisualObject.transform.position = interactionHit.Point;

            if (_newWall != null)
            {
                if (_currentHoveredWall != null)
                {
                    _newWall.SetEndPoint(_currentHoveredWall.GetClosestCenterPoint(interactionHit.Point));
                }
                else
                {
                    _newWall.SetEndPoint(interactionHit.Point);
                }
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

                _newWall.SetEndPoint(interactionHit.Point);

                _newWall.UpdateMeshCollider();

                _confirmPointInteractable.transform.position = hitPoint;

                _confirmPointInteractable.gameObject.SetActive(true);

                _wallStartPoint = interactionHit.Point;

                _prevWall = _newWall;

                _newWall = CreateNewWall();
            }
        }

        private void Confirm()
        {
            _confirmPointInteractable.gameObject.SetActive(false);

            Destroy(_newWall.gameObject);

            Selectable newWallSelectable = _prevWall.GetComponent<Selectable>();

            newWallSelectable.SelectOnly();

            Exit(hasSelectionStateSO);
        }

        private Wall CreateNewWall()
        {
            Wall newWall = Instantiate(newWallPrefab);
            newWall.transform.SetParent(wallsParent, true);
            newWall.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            newWall.Move(_wallStartPoint);

            return newWall;
        }

    }
}
