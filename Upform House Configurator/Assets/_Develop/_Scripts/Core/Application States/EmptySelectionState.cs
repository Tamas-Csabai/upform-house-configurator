using Upform.Selection;
using Upform.States;
using UnityEngine;

namespace Upform.Core
{
    public class EmptySelectionState : StateBase
    {

        [SerializeField] private StateSO hasSelectionStateSO;

        public override void OnEntering()
        {
            SelectionManager.OnMainSelectionChanged += MainSelectionChanged;
        }

        public override void Evaluate()
        {
            
        }

        public override void OnExiting()
        {
            SelectionManager.OnMainSelectionChanged -= MainSelectionChanged;
        }

        private void MainSelectionChanged(Selectable selectable)
        {
            if(selectable != null)
            {
                Exit(hasSelectionStateSO);
            }
        }
    }
}
