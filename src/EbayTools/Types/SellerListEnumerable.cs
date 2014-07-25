using System.Collections;
using System.Collections.Generic;
using System.Linq;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public class SellerListEnumerable : IEnumerable<ActiveItem>
    {
        private readonly IService _service;
        private readonly int _daysRange;
        private readonly List<ActiveItem> _cachedItems;
        private int _lastLoadedRange;
        private bool _fullyLoaded;

        public SellerListEnumerable(IService service, int daysRange = 120)
        {
            _service = service;
            _daysRange = daysRange;
            _cachedItems = new List<ActiveItem>();
            _fullyLoaded = false;
            _lastLoadedRange = -_daysRange;
        }

        public IEnumerator<ActiveItem> GetEnumerator()
        {
            var i = 0;
            ActiveItem item;
            while ((item = Get(i++)) != null)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Preload()
        {
            while (!_fullyLoaded)
                LoadNextPage();
        }

        private void LoadNextPage()
        {
            _lastLoadedRange += _daysRange;
            var list = _service.GetSellerList(_lastLoadedRange, _daysRange);
            if (list.Count == 0)
            {
                _fullyLoaded = true;
                return;
            }
            _cachedItems.AddRange(Enumerable.Cast<ItemType>(list).Select(item => new ActiveItem(item)));
        }

        public ActiveItem Get(int i)
        {
            if (i < _cachedItems.Count)
                return _cachedItems[i];

            if (_fullyLoaded)
                return null;

            LoadNextPage();
            return Get(i);
        }
    }
}