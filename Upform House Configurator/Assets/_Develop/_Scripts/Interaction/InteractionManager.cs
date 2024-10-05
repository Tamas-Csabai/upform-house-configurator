
using System.Collections.Generic;

namespace Upform.Interaction
{
    public static class InteractionManager
    {

        private static HashSet<Interactor> _interactors = new();
        private static bool _isStarted = false;

        public static event System.Action<InteractionHit> OnInteract;
        public static event System.Action<InteractionHit> OnAction;

        public static void Subscribe(Interactor interactor)
        {
            interactor.OnInteract += InteractorInteract;
            interactor.OnAction += InteractorAction;

            _interactors.Add(interactor);

            if(_isStarted)
            {
                interactor.StartInteract();
            }
        }

        public static void Unsubscribe(Interactor interactor)
        {
            interactor.OnInteract -= InteractorInteract;
            interactor.OnAction -= InteractorAction;

            _interactors.Remove(interactor);

            interactor.StopInteract();
        }

        public static void StartAllInteractor()
        {
            _isStarted = true;

            foreach (Interactor interactor in _interactors)
            {
                interactor.StartInteract();
            }
        }

        public static void StopAllInteractor()
        {
            _isStarted = false;

            foreach (Interactor interactor in _interactors)
            {
                interactor.StopInteract();
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

    }
}
