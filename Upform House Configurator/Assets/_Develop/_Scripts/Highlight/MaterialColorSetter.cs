
using UnityEngine;

namespace Upform.Highlight
{
    public class MaterialColorSetter : HighlightEffectBase
    {

        [Tooltip("_BaseColor | _EmissionColor")]
        [SerializeField] private string colorPropertyName = "_BaseColor";
        [SerializeField] private int materialChannelIndex;
        [SerializeField] private Renderer[] renderers;

        private Color[] _initialColors;

        private void Awake()
        {
            _initialColors = new Color[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].materials[materialChannelIndex].HasColor(colorPropertyName))
                {
                    _initialColors[i] = renderers[i].materials[materialChannelIndex].GetColor(colorPropertyName);
                }
            }
        }

        public void SetColors(Color color)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                SetColor(renderers[i], color);
            }
        }

        public void ResetColors()
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                SetColor(renderers[i], _initialColors[i]);
            }
        }

        private void SetColor(Renderer renderer, Color color)
        {
            Material material = renderer.materials[materialChannelIndex];

            if (material.HasColor(colorPropertyName))
            {
                color.a = material.GetColor(colorPropertyName).a;
                material.SetColor(colorPropertyName, color);
            }
        }

        public override void ApplyEffect(HighlightSO highlightSO)
        {
            SetColors(highlightSO.Color);
        }

        public override void ClearEffect()
        {
            ResetColors();
        }

    }
}
