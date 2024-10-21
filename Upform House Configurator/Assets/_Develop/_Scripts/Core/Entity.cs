
using UnityEngine;
using Upform.Interaction;
using Upform.Selection;

namespace Upform.Core
{
    public abstract class Entity : MonoBehaviour
    {

        [SerializeField] protected Interactable interactable;
        [SerializeField] protected Selectable selectable;

        public Interactable Interactable => interactable;
        public Selectable Selectable => selectable;

        private void Awake()
        {
            interactable.OnInteract += Interact;
        }

        private void OnDestroy()
        {
            if(interactable != null)
            {
                interactable.OnInteract -= Interact;
            }
        }

        private void Interact()
        {
            if(interactable.LastInteractInteractionHit.IsAdditive)
            {
                selectable.AddToSelection();
            }
            else
            {
                selectable.SelectOnly();
            }
        }
    }
}
