using UnityEngine;
using Upform.Input;

namespace Upform.Interaction
{
    public struct InteractionHit
    {

        public static InteractionHit Empty = new InteractionHit()
        {
            HasHit = false,
            Interactable = null,
            InputModifier = InputModifier.None,
            Point = Vector3.zero,
            Normal = Vector3.up,
        };

        public bool HasHit;
        public Interactable Interactable;
        public InputModifier InputModifier;
        public Vector3 Point;
        public Vector3 Normal;

        public bool IsAdditive => (InputModifier & InputModifier.Additive) == Input.InputModifier.Additive;
        public bool IsAlternative => (InputModifier & InputModifier.Alternative) == Input.InputModifier.Alternative;
        public bool IsSubstractive => (InputModifier & InputModifier.Substractive) == Input.InputModifier.Substractive;
    }
}
