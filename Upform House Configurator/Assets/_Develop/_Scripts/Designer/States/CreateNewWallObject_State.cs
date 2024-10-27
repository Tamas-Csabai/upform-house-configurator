using UnityEngine;
using Upform.Selection;
using Upform.States;

namespace Upform.Designer
{
    public class CreateNewWallObject_State : StateBase
    {

        [SerializeField] private StateSO emptySelectionStateSO;
        [SerializeField] private StateSO hasSelectionStateSO;
        [SerializeField] private WallObjectSO wallObjectSO;
        [SerializeField] private WallObjectCreator wallObjectCreator;

        public override void OnEntering()
        {
            SelectionManager.ClearSelection();

            wallObjectCreator.OnNewWallObject += NewWallObject;
            wallObjectCreator.StartInteraction(wallObjectSO);
        }

        public override void OnExiting()
        {
            wallObjectCreator.StopInteraction();
            wallObjectCreator.OnNewWallObject -= NewWallObject;
        }

        private void NewWallObject(WallObject wallObject)
        {
            if(wallObject != null)
            {
                SelectionManager.SelectOnly(wallObject.Entity.Selectable);

                Exit(hasSelectionStateSO);
            }
            else
            {
                SelectionManager.ClearSelection();

                Exit(emptySelectionStateSO);
            }
        }
    }
}
