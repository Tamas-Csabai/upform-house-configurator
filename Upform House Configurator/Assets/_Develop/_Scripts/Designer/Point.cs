using TMPro;
using UnityEngine;

namespace Upform.Designer
{
    public class Point : MonoBehaviour
    {

        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private MeshRenderer meshRenderer;

        public void ShowPoint()
        {
            meshRenderer.enabled = true;
        }

        public void HidePoint()
        {
            meshRenderer.enabled = false;
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }

        public void ShowText()
        {
            textMeshPro.enabled = true;
        }

        public void HideText()
        {
            textMeshPro.enabled = false;
        }

    }
}
