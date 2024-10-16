
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
        [SerializeField] private Interactable designerSheetInteractable;

        private Coroutine _placeWallEndPoint_Routine;
        private bool _isStartPointPlaced = false;
        private Vector3 _wallStartPoint;
        private Vector3 _wallEndPoint;
        private Vector3 _hoverPoint;

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
            designerSheetInteractable.OnHovering += Hovering;

            _newWall = null;
            _isStartPointPlaced = false;
            _wallStartPoint = Vector3.zero;
            _wallEndPoint = Vector3.zero;

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
            designerSheetInteractable.OnHovering -= Hovering;
        }

        private void Hovering()
        {
            _hoverPoint = designerSheetInteractable.LastHoverInteractionHit.Point;
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
                _wallEndPoint = interactionHit.Point;

                Selectable newWallSelectable = _newWall.GetComponent<Selectable>();

                newWallSelectable.SelectOnly();

                Exit(hasSelectionStateSO);
            }
        }

        private Wall CreateNewWall()
        {
            Wall newWall = Instantiate(newWallPrefab);

            newWall.transform.position = _wallStartPoint;

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

                yield return null;
            }
        }


    }
}
