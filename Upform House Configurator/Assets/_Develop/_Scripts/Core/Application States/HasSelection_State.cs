
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
        [SerializeField] private IntersectionMover intersectionMover;

        private Intersection _currentIntersection;
        private Selectable _currentSelectable;
        private Interactable _currentInteractable;

        public override void OnEntering()
        {
            SelectionManager.OnMainSelectionChanged += MainSelectionChanged;

            MainSelectionChanged(SelectionManager.CurrentSelectionGroup.Main);
        }

        private void StartMove()
        {
            InteractionManager.OnInteract += StopMove;

            intersectionMover.StartInteraction(_currentIntersection);
        }

        private void StopMove(InteractionHit hit)
        {
            InteractionManager.OnInteract -= StopMove;

            intersectionMover.StopInteraction();
        }

        public override void OnExiting()
        {
            SelectionManager.OnMainSelectionChanged -= MainSelectionChanged;
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

            _currentIntersection = _currentSelectable.GetComponent<Intersection>();

            if (_currentInteractable != null)
            {
                _currentInteractable.OnInteractDown -= StartMove;
            }

            _currentInteractable = _currentSelectable.GetComponent<Interactable>();
            _currentInteractable.OnInteractDown += StartMove;
        }
    }
}
