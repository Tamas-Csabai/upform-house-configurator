
using UnityEngine;

namespace Upform.Interaction
{
    public class Interactable : MonoBehaviour
    {

        [SerializeField] private InteractionLayer layer = InteractionLayer.Default;

        private bool _isEnabled = true;
        private bool _isLeftClickInside = false;
        private bool _isRightClickInside = false;

        public event System.Action OnHoverEnter;
        public event System.Action OnHovering;
        public event System.Action OnHoverExit;

        public event System.Action OnInteract;
        public event System.Action OnInteractDown;
        public event System.Action OnInteractUp;

        public event System.Action OnAction;
        public event System.Action OnActionDown;
        public event System.Action OnActionUp;

        public event System.Action OnEnabled;
        public event System.Action OnDisabled;

        public InteractionLayer Layer => layer;

        public Collider Collider { get; private set; }

        public bool IsHovering { get; private set; }

        public bool IsEnabled
        {
            get => _isEnabled;

            set
            {
                bool wasEnabled = _isEnabled;

                _isEnabled = value;

                if (value)
                {
                    if (!wasEnabled)
                        Enabled();
                }
                else
                {
                    if (wasEnabled)
                        Disabled();
                }
            }
        }

        public InteractionHit LastHoverInteractionHit { get; protected set; }
        public InteractionHit LastInteractInteractionHit { get; protected set; }
        public InteractionHit LastActionInteractionHit { get; protected set; }

        private void Start()
        {
            Enabled();
        }

        private void Enabled()
        {
            OnEnabled?.Invoke();
        }

        private void Disabled()
        {
            IsHovering = false;

            _isLeftClickInside = false;
            _isRightClickInside = false;

            OnDisabled?.Invoke();
        }

        public bool HasLayer(InteractionLayer interactionLayer)
        {
            return (layer & interactionLayer) == interactionLayer;
        }

        public void HoverEnter(InteractionHit interactionHit)
        {
            if (!_isEnabled)
                return;

            LastHoverInteractionHit = interactionHit;

            IsHovering = true;

            OnHoverEnter?.Invoke();
        }

        public void Hovering(InteractionHit interactionHit)
        {
            if (!_isEnabled)
                return;

            LastHoverInteractionHit = interactionHit;

            OnHovering?.Invoke();
        }

        public void HoverExit(InteractionHit interactionHit)
        {
            if (!_isEnabled)
                return;

            LastHoverInteractionHit = interactionHit;

            IsHovering = false;

            _isLeftClickInside = false;
            _isRightClickInside = false;

            OnHoverExit?.Invoke();
        }

        public void InteractDown(InteractionHit interactionHit)
        {
            LastInteractInteractionHit = interactionHit;

            _isLeftClickInside = true;

            if (!_isEnabled)
                return;

            OnInteractDown?.Invoke();
        }

        public void InteractUp(InteractionHit interactionHit)
        {
            LastInteractInteractionHit = interactionHit;

            if (_isLeftClickInside)
            {
                _isLeftClickInside = false;

                if (_isEnabled)
                {
                    Interact();
                }
            }

            if (!_isEnabled)
                return;

            OnInteractUp?.Invoke();
        }

        public void ActionDown(InteractionHit interactionHit)
        {
            LastActionInteractionHit = interactionHit;

            _isRightClickInside = true;

            if (!_isEnabled)
                return;

            OnActionDown?.Invoke();
        }

        public void ActionUp(InteractionHit interactionHit)
        {
            LastActionInteractionHit = interactionHit;

            if (_isRightClickInside)
            {
                _isRightClickInside = false;

                if (_isEnabled)
                {
                    Action();
                }
            }

            if (!_isEnabled)
                return;

            OnActionUp?.Invoke();
        }

        private void Interact()
        {
            OnInteract?.Invoke();
        }

        private void Action()
        {
            OnAction?.Invoke();
        }

    }
}
