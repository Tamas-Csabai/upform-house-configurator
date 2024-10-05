
namespace Upform.Input
{
    public class InputActionProvider_Desktop : InputActionProvider
    {

        public override ButtonAction DebugMode { get; protected set; } = new(() => UnityEngine.Input.GetKey(UnityEngine.KeyCode.F2));

        public override bool IsAdditive => UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift);
        public override bool IsSubstractive => UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl);
        public override bool IsAlternative => UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftAlt);

        public override ButtonAction Interact { get; protected set; } = new(() => UnityEngine.Input.GetKey(UnityEngine.KeyCode.Mouse0));
        public override ButtonAction Action { get; protected set; } = new(() => UnityEngine.Input.GetKey(UnityEngine.KeyCode.Mouse1));
        public override ButtonAction Back { get; protected set; } = new(() => UnityEngine.Input.GetKey(UnityEngine.KeyCode.Escape));

        public override ButtonAction CameraMove { get; protected set; } = new(() => UnityEngine.Input.GetKey(UnityEngine.KeyCode.Mouse2));
        public override SingleAction CameraZoomDelta { get; protected set; } = new(() => UnityEngine.Input.mouseScrollDelta.y);

        public override Vector3Action PointerPosition { get; protected set; } = new(() => UnityEngine.Input.mousePosition);
        
    }
}
