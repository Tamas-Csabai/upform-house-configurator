
namespace Upform.Interaction
{
    [System.Flags]
    public enum InteractionLayer
    {
        Default = 1,
        Intersection = 2,
        Wall = 4,
        WallObject = 8
    }
}
