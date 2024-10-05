using Upform.Input;
using System.Collections;
using UnityEngine;

namespace Upform.Interaction
{
    public abstract class Interactor : MonoBehaviour
    {

        private Coroutine _interactRoutine;
        private InteractionHit _currentInteractionHit;
        private Interactable _currentInteractable;

#if UNITY_EDITOR
        private Rect _EDITOR_DebugWindowRect = new Rect(20f, 20f, 300f, 50f);
#endif

        public event System.Action<InteractionHit> OnInteract;
        public event System.Action<InteractionHit> OnAction;

        protected virtual void Awake()
        {
            InputManager.Actions.Interact.OnDown += InteractDown;
            InputManager.Actions.Interact.OnUp += InteractUp;
            InputManager.Actions.Action.OnDown += ActionDown;
            InputManager.Actions.Action.OnUp += ActionUp;

            InteractionManager.Subscribe(this);
        }

        protected virtual void OnDestroy()
        {
            InputManager.Actions.Interact.OnDown -= InteractDown;
            InputManager.Actions.Interact.OnUp -= InteractUp;
            InputManager.Actions.Action.OnDown -= ActionDown;
            InputManager.Actions.Action.OnUp -= ActionUp;

            InteractionManager.Unsubscribe(this);
        }

        public void StartInteract()
        {
            if(_interactRoutine != null)
            {
                StopCoroutine(_interactRoutine);
            }

            _interactRoutine = StartCoroutine(Interact_Routine());
        }

        public void StopInteract()
        {
            if (_interactRoutine != null)
            {
                StopCoroutine(_interactRoutine);
            }

            _interactRoutine = null;
        }

        protected IEnumerator Interact_Routine()
        {
            while (enabled)
            {
                _currentInteractionHit = Interact();

                if (_currentInteractionHit.HasHit && _currentInteractionHit.Interactable != null)
                {
                    if (_currentInteractionHit.Interactable != _currentInteractable)
                    {
                        if(_currentInteractable != null)
                        {
                            _currentInteractable.HoverExit(_currentInteractionHit);
                        }

                        _currentInteractable = _currentInteractionHit.Interactable;

                        _currentInteractable.HoverEnter(_currentInteractionHit);
                    }
                }
                else
                {
                    if(_currentInteractable != null)
                    {
                        _currentInteractable.HoverExit(_currentInteractionHit);

                        _currentInteractable = null;
                    }
                }

                yield return null;
            }
        }

        protected abstract InteractionHit Interact();

        private void InteractDown(InputModifier inputModifier)
        {
            if (_currentInteractable != null)
            {
                _currentInteractionHit.InputModifier = inputModifier;
                _currentInteractable.InteractDown(_currentInteractionHit);
            }
        }

        private void InteractUp(InputModifier inputModifier)
        {
            if (_currentInteractable != null)
            {
                _currentInteractionHit.InputModifier = inputModifier;
                _currentInteractable.InteractUp(_currentInteractionHit);
            }

            if (_currentInteractionHit.HasHit)
            {
                OnInteract?.Invoke(_currentInteractionHit);
            }
        }

        private void ActionDown(InputModifier inputModifier)
        {
            if (_currentInteractable != null)
            {
                _currentInteractionHit.InputModifier = inputModifier;
                _currentInteractable.ActionDown(_currentInteractionHit);
            }
        }

        private void ActionUp(InputModifier inputModifier)
        {
            if (_currentInteractable != null)
            {
                _currentInteractionHit.InputModifier = inputModifier;
                _currentInteractable.ActionUp(_currentInteractionHit);
            }

            if (_currentInteractionHit.HasHit)
            {
                OnAction?.Invoke(_currentInteractionHit);
            }
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (!Debugger.ShowWindows)
                return;

            _EDITOR_DebugWindowRect = GUI.Window(GetInstanceID(), _EDITOR_DebugWindowRect, EDITOR_DrawWindow, "Camera Interactor");
        }

        private void EDITOR_DrawWindow(int id)
        {
            string currentHitName = "nothing";

            if (_currentInteractable != null)
            {
                currentHitName = _currentInteractable.name;
            }

            GUILayout.Label("Current hit: " + currentHitName);

            GUI.DragWindow();
        }
#endif

    }
}
