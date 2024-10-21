using TMPro;
using UnityEngine;
using Upform.Core;
using Upform.Interaction;

namespace Upform.Designer
{
    public class Point : EntityComponent
    {

        [SerializeField] private Interactable interactable;
        [SerializeField] private TextMeshPro textMeshPro;
        [SerializeField] private MeshRenderer meshRenderer;

        public Interactable Interactable => interactable;

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
