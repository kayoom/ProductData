using System.Collections;
using System.Collections.Generic;

namespace EbayTools.Types
{
    public class ActiveItemsEnumerator : IEnumerator<ActiveItem>
    {
        private readonly ActiveItemsEnumerable _items;
        private int _index;

        public ActiveItemsEnumerator(ActiveItemsEnumerable items)
        {
            _items = items;
        }

        #region IEnumerator<ActiveItem> Members

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            Current = _items.Get(_index++);
            return Current != null;
        }

        public void Reset()
        {
            _index = 0;
        }

        public ActiveItem Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion
    }
}