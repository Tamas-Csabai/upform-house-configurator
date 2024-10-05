
namespace Upform.Selection
{
    public static class SelectionManager
    {

        public static event System.Action<Selectable> OnSelection;
        public static event System.Action<Selectable> OnDeselection;
        public static event System.Action<Selectable> OnMainSelectionChanged;
        public static event System.Action OnClearSelection;

        public static SelectionGroup CurrentSelectionGroup { get; private set; } = new();

        public static void AddToSelection(Selectable selectable)
        {
            if (CurrentSelectionGroup.Contains(selectable))
            {
                RemoveFromSelection(selectable);
            }
            else
            {
                CurrentSelectionGroup.Add(selectable);

                OnMainSelectionChanged?.Invoke(selectable);

                OnSelection?.Invoke(selectable);
            }
        }

        public static void RemoveFromSelection(Selectable selectable)
        {
            CurrentSelectionGroup.Remove(selectable);

            OnMainSelectionChanged?.Invoke(CurrentSelectionGroup.Main);

            OnDeselection?.Invoke(selectable);
        }

        public static void SelectOnly(Selectable selectable)
        {
            ClearCurrentSelection();

            AddToSelection(selectable);
        }

        public static void ClearSelection()
        {
            ClearCurrentSelection();

            OnClearSelection?.Invoke();
        }

        private static void ClearCurrentSelection()
        {
            CurrentSelectionGroup.Clear();

            OnMainSelectionChanged?.Invoke(null);
        }

    }
}
