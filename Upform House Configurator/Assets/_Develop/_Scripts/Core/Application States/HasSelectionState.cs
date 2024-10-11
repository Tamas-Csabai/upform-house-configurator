
using Upform.Interaction;
using Upform.Selection;
using Upform.States;
using UnityEngine;

namespace Upform.Core
{
    public class HasSelectionState : StateBase
    {

        [SerializeField] private StateSO emptySelectionStateSO;

        public override void OnEntering()
        {
            SelectionManager.OnMainSelectionChanged += MainSelectionChanged;
            InteractionManager.OnAction += InteractionAction;
        }

        public override void Evaluate()
        {
            
        }

        public override void OnExiting()
        {
            SelectionManager.OnMainSelectionChanged -= MainSelectionChanged;
            InteractionManager.OnAction -= InteractionAction;
        }

        private void InteractionAction(InteractionHit interactionHit)
        {
            
        }

        private void MainSelectionChanged(Selectable selectable)
        {
            if(selectable == null)
            {
                Exit(emptySelectionStateSO);
            }
        }
    }
}
