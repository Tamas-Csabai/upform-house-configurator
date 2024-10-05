
using UnityEngine;
using Upform.Interaction;

namespace Upform.Highlight
{
    public class HoverHighlight : HighlightBase
    {

        [SerializeField] private Interactable interactable;

        private void Awake()
        {
            interactable.OnHoverEnter += HoverEnter;
            interactable.OnHoverExit += HoverExit;
        }

        private void OnDestroy()
        {
            if(interactable != null)
            {
                interactable.OnHoverEnter -= HoverEnter;
                interactable.OnHoverExit -= HoverExit;
            }
        }

        private void HoverEnter()
        {
            AddHighlight();
        }

        private void HoverExit()
        {
            RemoveHighlight();
        }
    }
}
