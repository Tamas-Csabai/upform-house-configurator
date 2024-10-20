
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
        [SerializeField] private Wall newWallPrefab;
        [SerializeField] private Transform wallsParent;
        [SerializeField] private Interactable designerSheetInteractable;

        private Coroutine _placeWallEndPoint_Routine;
        private bool _isStartPointPlaced = false;
        private Vector3 _wallStartPoint;
        private Vector3 _hoverPoint;
        private Wall _currentHoveredWall;

        private GameObject _pointVisualObject;
        private Wall _newWall;

        public override void OnEntering()
        {
            if(_pointVisualObject == null)
            {
                _pointVisualObject = Instantiate(pointVisualObjectPrefab);
                _pointVisualObject.transform.SetParent(transform, true);
                _pointVisualObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _pointVisualObject.SetActive(true);

            SelectionManager.ClearSelection();

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;

            _currentHoveredWall = null;
            _newWall = null;
            _isStartPointPlaced = false;
            _wallStartPoint = Vector3.zero;

            StartPlaceWallEndPoint();
        }

        public override void Evaluate()
        {

        }

        public override void OnExiting()
        {
            _pointVisualObject.SetActive(false);

            StopPlaceWallEndPoint();

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
            _hoverPoint = interactionHit.Point;
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
                _newWall.SetEndPoint(interactionHit.Point);

                _newWall.UpdateMeshCollider();

                Selectable newWallSelectable = _newWall.GetComponent<Selectable>();

                newWallSelectable.SelectOnly();

                Exit(hasSelectionStateSO);
            }
        }

        private Wall CreateNewWall()
        {
            Wall newWall = Instantiate(newWallPrefab);
            newWall.transform.SetParent(wallsParent, true);
            newWall.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            newWall.Move(_wallStartPoint);

            return newWall;
        }

        private void StartPlaceWallEndPoint()
        {
            if(_placeWallEndPoint_Routine != null)
            {
                StopCoroutine(_placeWallEndPoint_Routine);
            }

            _placeWallEndPoint_Routine = StartCoroutine(PlaceWallEndPoint_Routine());
        }

        private void StopPlaceWallEndPoint()
        {
            if (_placeWallEndPoint_Routine != null)
            {
                StopCoroutine(_placeWallEndPoint_Routine);
            }

            _placeWallEndPoint_Routine = null;
        }

        private IEnumerator PlaceWallEndPoint_Routine()
        {
            while (true)
            {
                _pointVisualObject.transform.position = _hoverPoint;

                if(_newWall != null)
                {
                    _newWall.SetEndPoint(_hoverPoint);
                }

                if(_currentHoveredWall != null)
                {
                    _newWall.SetEndPoint(_currentHoveredWall.GetClosestCenterPoint(_hoverPoint));
                }

                yield return null;
            }
        }


    }
}
