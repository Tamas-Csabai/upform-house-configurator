
namespace Upform.Input
{
    public abstract class InputActionProvider
    {

#if UNITY_EDITOR
        public abstract ButtonAction DebugMode { get; protected set; }
#else
        public ButtonAction DebugMode { get; protected set; } = new(() => false);
#endif

        public abstract bool IsAdditive { get; }
        public abstract bool IsSubstractive { get; }
        public abstract bool IsAlternative { get; }

        public abstract ButtonAction Interact { get; protected set; }
        public abstract ButtonAction Action { get; protected set; }
        public abstract ButtonAction Back { get; protected set; }

        public abstract ButtonAction CameraMove { get; protected set; }
        public abstract SingleAction CameraZoomDelta { get; protected set; }

        public abstract Vector3Action PointerPosition { get; protected set; }

    }
}
