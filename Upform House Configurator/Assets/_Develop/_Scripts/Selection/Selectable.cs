using UnityEngine;

namespace Upform.Selection
{
    public class Selectable : MonoBehaviour
    {

        private bool _isSelected = false;

        public event System.Action OnSelected;
        public event System.Action OnDeselected;

        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                if(value)
                {
                    if (!_isSelected)
                    {
                        _isSelected = true;

                        Selected();
                    }
                }
                else
                {
                    if(_isSelected)
                    {
                        _isSelected = false;

                        Deselected();
                    }
                }
            }
        }

        public void SelectOnly()
        {
            SelectionManager.SelectOnly(this);
        }

        public void AddToSelection()
        {
            SelectionManager.AddToSelection(this);
        }

        private void Selected()
        {
            OnSelected?.Invoke();
        }

        private void Deselected()
        {
            OnDeselected?.Invoke();
        }

    }
}
