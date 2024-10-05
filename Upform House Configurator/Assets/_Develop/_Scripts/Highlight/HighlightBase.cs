using UnityEngine;

namespace Upform.Highlight
{
    public abstract class HighlightBase : MonoBehaviour
    {

        [SerializeField] private HighlightHandler highlightHandler;
        [SerializeField] private HighlightSO highlightSO;

        protected void AddHighlight()
        {
            highlightHandler.AddHighlight(highlightSO);
        }

        protected void RemoveHighlight()
        {
            highlightHandler.RemoveHighlight(highlightSO);
        }
    }
}
