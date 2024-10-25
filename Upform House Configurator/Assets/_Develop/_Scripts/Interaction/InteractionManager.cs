
using System.Collections.Generic;

namespace Upform.Interaction
{
    public static class InteractionManager
    {

        private static HashSet<Interactor> _interactors = new();

        public static event System.Action<InteractionHit> OnInteract;
        public static event System.Action<InteractionHit> OnAction;
        public static event System.Action<InteractionHit> OnHovering;
        public static event System.Action<InteractionHit> OnHoverEnter;
        public static event System.Action<InteractionHit> OnHoverExit;

        public static void Subscribe(Interactor interactor)
        {
            interactor.OnInteract += InteractorInteract;
            interactor.OnAction += InteractorAction;
            interactor.OnHovering += InteractorHovering;
            interactor.OnHoverEnter += InteractorHoverEnter;
            interactor.OnHoverExit += InteractorHoverExit;

            _interactors.Add(interactor);
        }

        public static void Unsubscribe(Interactor interactor)
        {
            interactor.OnInteract -= InteractorInteract;
            interactor.OnAction -= InteractorAction;
            interactor.OnHovering -= InteractorHovering;
            interactor.OnHoverEnter -= InteractorHoverEnter;
            interactor.OnHoverExit -= InteractorHoverExit;

            _interactors.Remove(interactor);
        }

        public static void FetchInteractions()
        {
            foreach (Interactor interactor in _interactors)
            {
                interactor.FetchInteraction();
            }
        }

        private static void InteractorInteract(InteractionHit interactionHit)
        {
            OnInteract?.Invoke(interactionHit);
        }

        private static void InteractorAction(InteractionHit interactionHit)
        {
            OnAction?.Invoke(interactionHit);
        }

        private static void InteractorHovering(InteractionHit interactionHit)
        {
            OnHovering?.Invoke(interactionHit);
        }

        private static void InteractorHoverEnter(InteractionHit interactionHit)
        {
            OnHoverEnter?.Invoke(interactionHit);
        }

        private static void InteractorHoverExit(InteractionHit interactionHit)
        {
            OnHoverExit?.Invoke(interactionHit);
        }

    }
}
