
using System.Collections.Generic;

namespace Upform.Selection
{
    public class SelectionGroup
    {

        private LinkedList<Selectable> _selectables = new();

        public int Count => _selectables.Count;

        public Selectable Main { get; private set; }

        public bool Contains(Selectable selectable)
        {
            return _selectables.Contains(selectable);
        }

        public void Add(Selectable selectable)
        {
            _selectables.AddLast(selectable);

            Main = selectable;

            selectable.IsSelected = true;
        }

        public void Remove(Selectable selectable)
        {
            _selectables.Remove(selectable);

            if(Main == selectable)
            {
                if(_selectables.Count > 0)
                {
                    Main = _selectables.Last.Value;
                }
                else
                {
                    Main = null;
                }
            }

            selectable.IsSelected = false;
        }

        public void Clear()
        {
            Main = null;

            foreach (Selectable selectable in _selectables)
            {
                selectable.IsSelected = false;
            }

            _selectables.Clear();
        }

        public void Foreach(System.Action<Selectable> action)
        {
            foreach (Selectable selectable in _selectables)
            {
                action.Invoke(selectable);
            }
        }
    }
}
