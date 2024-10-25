using Upform.Input;
using Upform.Interaction;
using UnityEngine;
using Upform.States;

namespace Upform.Core
{
    public class ApplicationController : MonoBehaviour
    {

#if UNITY_EDITOR
        private Rect _EDITOR_DebugWindowRect = new Rect(20f, 100f, 300f, 50f);
#endif

        [SerializeField] private StateMachine stateMachine;

        private void Start()
        {
            stateMachine.EnterDefaultState();
        }

        private void Update()
        {
            InputManager.Poll();

            InteractionManager.FetchInteractions();
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
            GUILayout.Label("Current hit: " + stateMachine.CurrentState.StateSO.name);

            GUI.DragWindow();
        }
#endif

    }
}
