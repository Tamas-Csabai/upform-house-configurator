using UnityEngine;

namespace Upform.Highlight
{
    [CreateAssetMenu(fileName = "New HighlightSO", menuName = "Upform/New HighlightSO")]
    public class HighlightSO : ScriptableObject
    {

        public int Priority;

        public Color Color;

    }
}
