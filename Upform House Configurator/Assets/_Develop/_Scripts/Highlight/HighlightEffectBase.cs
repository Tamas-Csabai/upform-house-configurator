using UnityEngine;

namespace Upform.Highlight
{
    public abstract class HighlightEffectBase : MonoBehaviour
    {

        public abstract void ApplyEffect(HighlightSO highlightSO);

        public abstract void ClearEffect();

    }
}
