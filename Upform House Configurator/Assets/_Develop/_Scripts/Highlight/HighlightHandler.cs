using System.Collections.Generic;
using UnityEngine;

namespace Upform.Highlight
{
    public class HighlightHandler : MonoBehaviour
    {

        [SerializeField] private HighlightEffectBase[] highlightEffectBases;

        private LinkedList<HighlightSO> _currentHighlightSOs = new();

        public void AddHighlight(HighlightSO highlightSO)
        {
            LinkedListNode<HighlightSO> highlightSONode = _currentHighlightSOs.First;

            while(highlightSONode != null)
            {
                if (highlightSONode.Value.Priority <= highlightSO.Priority)
                {
                    highlightSONode = _currentHighlightSOs.AddBefore(highlightSONode, highlightSO);

                    break;
                }

                highlightSONode = highlightSONode.Next;
            }

            if(highlightSONode == null)
            {
                highlightSONode = _currentHighlightSOs.AddLast(highlightSO);
            }

            if (highlightSONode == _currentHighlightSOs.First)
            {
                ApplyHighlight(highlightSO);
            }
        }

        public void RemoveHighlight(HighlightSO highlightSO)
        {
            if(_currentHighlightSOs.First.Value == highlightSO)
            {
                if(_currentHighlightSOs.First.Next != null)
                {
                    ApplyHighlight(_currentHighlightSOs.First.Next.Value);
                }
                else
                {
                    ClearHighlight();
                }
            }

            _currentHighlightSOs.Remove(highlightSO);
        }

        public void ClearAllHighlight()
        {
            _currentHighlightSOs.Clear();

            ClearHighlight();
        }

        private void ApplyHighlight(HighlightSO highlightSO)
        {
            for (int i = 0; i < highlightEffectBases.Length; i++)
            {
                highlightEffectBases[i].ApplyEffect(highlightSO);
            }
        }

        private void ClearHighlight()
        {
            for (int i = 0; i < highlightEffectBases.Length; i++)
            {
                highlightEffectBases[i].ClearEffect();
            }
        }
    }
}
