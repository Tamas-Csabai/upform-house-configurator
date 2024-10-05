using UnityEngine;

namespace Upform.Interaction
{
    public struct InteractionHit
    {

        public static InteractionHit Empty = new InteractionHit()
        {
            HasHit = false,
            Interactable = null,
            Point = Vector3.zero,
            Normal = Vector3.up,
        };

        public bool HasHit;
        public Interactable Interactable;
        public Vector3 Point;
        public Vector3 Normal;
    }
}
