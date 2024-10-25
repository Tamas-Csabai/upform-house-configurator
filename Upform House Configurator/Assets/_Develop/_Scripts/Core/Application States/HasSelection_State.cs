
using Upform.Interaction;
using Upform.Selection;
using Upform.States;
using UnityEngine;
using System.Collections;
using Upform.Designer;

namespace Upform.Core
{
    public class HasSelection_State : StateBase
    {

        [SerializeField] private StateSO emptySelectionStateSO;

        private Coroutine _moveCurrentSelectable_Routine;
        private RectangleLine _currentWall;
        private Selectable _currentSelectable;
        private Interactable _currentInteractable;
        private Vector3 _hoverPosition;
        private Vector3 _moveStartHoverPosition;
        private Vector3 _wallStartPosition;
        private Vector3 _offset;
        private Vector3 _perpendicular;

        public override void OnEntering()
        {
            SelectionManager.OnMainSelectionChanged += MainSelectionChanged;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnInteract += StopMove;

            MainSelectionChanged(SelectionManager.CurrentSelectionGroup.Main);
        }

        private void Hovering(InteractionHit hit)
        {
            _hoverPosition = hit.Point;
        }

        private void StartMove()
        {
            _moveStartHoverPosition = _hoverPosition;
            _wallStartPosition = _currentWall.transform.position;
            _perpendicular = _currentWall.Perpendicular;

            if (_moveCurrentSelectable_Routine != null)
            {
                StopCoroutine(_moveCurrentSelectable_Routine);
            }

            _moveCurrentSelectable_Routine = StartCoroutine(MoveCurrentSelectable_Routine());
        }

        private void StopMove(InteractionHit hit)
        {
            if (_moveCurrentSelectable_Routine != null)
            {
                StopCoroutine(_moveCurrentSelectable_Routine);
            }

            _moveCurrentSelectable_Routine = null;
        }

        private IEnumerator MoveCurrentSelectable_Routine()
        {
            while (true)
            {
                Vector3 targetPosition = Vector3.Project(_hoverPosition - _moveStartHoverPosition, _perpendicular);
                targetPosition.y = 0f;
                _currentWall.transform.position = _wallStartPosition + targetPosition;

                yield return null;
            }
        }

        public override void OnExiting()
        {
            SelectionManager.OnMainSelectionChanged -= MainSelectionChanged;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnInteract -= StopMove;

            if (_currentInteractable != null)
            {
                _currentInteractable.OnInteractDown -= StartMove;
            }
        }

        private void MainSelectionChanged(Selectable selectable)
        {
            if(selectable == null)
            {
                Exit(emptySelectionStateSO);
                return;
            }

            _currentSelectable = selectable;

            _currentWall = _currentSelectable.GetComponent<RectangleLine>();

            if (_currentInteractable != null)
            {
                _currentInteractable.OnInteractDown -= StartMove;
            }

            _currentInteractable = _currentSelectable.GetComponent<Interactable>();
            _currentInteractable.OnInteractDown += StartMove;
        }
    }
}
