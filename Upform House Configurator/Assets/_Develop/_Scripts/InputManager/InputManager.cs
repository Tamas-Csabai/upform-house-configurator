
using System.Collections.Generic;

namespace Upform.Input
{
    public static class InputManager
    {

        private static HashSet<InputActionBase> _InputActions = new();

        public static InputActionProvider Actions { get; private set; } = new InputActionProvider_Desktop();

        public static void Poll()
        {
            InputModifier inputModifier = InputModifier.None;

            if (Actions.IsAdditive)
                inputModifier |= InputModifier.Additive;

            if (Actions.IsSubstractive)
                inputModifier |= InputModifier.Substractive;

            if (Actions.IsAlternative)
                inputModifier |= InputModifier.Alternative;

            foreach (InputActionBase action in _InputActions)
                action.Set(inputModifier);
        }

        public static void SubscribeAction(InputActionBase action)
        {
            _InputActions.Add(action);
        }

        public static void UnsubscribeAction(InputActionBase action)
        {
            _InputActions.Remove(action);
        }

    }
}
