using UnityEngine;

namespace Upform.Designer
{
    [CreateAssetMenu(fileName = "New WallObjectSO", menuName = "Upform/New WallObjectSO")]
    public class WallObjectSO : ScriptableObject
    {

        public string Name;

        public float Width;

        public float Height;
    }
}
