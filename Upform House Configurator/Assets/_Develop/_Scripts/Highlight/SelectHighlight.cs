using UnityEngine;
using Upform.Selection;

namespace Upform.Highlight
{
    public class SelectHighlight : HighlightBase
    {

        [SerializeField] private Selectable selectable;

        private void Awake()
        {
            selectable.OnSelected += Selected;
            selectable.OnDeselected += Deselected;
        }

        private void OnDestroy()
        {
            if (selectable != null)
            {
                selectable.OnSelected -= Selected;
                selectable.OnDeselected -= Deselected;
            }
        }

        private void Selected()
        {
            AddHighlight();
        }

        private void Deselected()
        {
            RemoveHighlight();
        }

    }
}
