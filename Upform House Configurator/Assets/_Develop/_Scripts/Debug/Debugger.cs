using UnityEngine;

namespace Upform
{
    public class Debugger : MonoBehaviour
    {
        private static Debugger _Instance;

        public static bool ShowGizmos { get; private set; } = false;
        public static bool ShowWindows { get; private set; } = false;

        private bool _isEnabled = false;

#if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            if (_Instance != null)
                return;

            _Instance = new GameObject("--- [ DEBUGGER ] ---").AddComponent<Debugger>();

            DontDestroyOnLoad(_Instance);
        }

#endif

        private void Update()
        {
            if (Input.InputManager.Actions.DebugMode.GetUp())
            {
                SwitchEnabled();
            }
        }

        private void SwitchEnabled()
        {
            _isEnabled = !_isEnabled;

            if (_isEnabled)
            {
                ShowGizmos = true;
                ShowWindows = true;
            }
            else
            {
                ShowGizmos = false;
                ShowWindows = false;
            }
        }
    }
}
