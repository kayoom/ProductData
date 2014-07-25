﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public class PaginatedEnumerable<T> : IEnumerable<T> where T : class 
    {
        private readonly Func<int, int, IEnumerable<T>> _func;
        private readonly int _batchSize;
        private readonly List<T> _cachedItems;
        private int _lastLoadedPage;
        private bool _fullyLoaded;

        public PaginatedEnumerable(Func<int, int, IEnumerable<T>> func, int batchSize = 200)
        {
            _func = func;
            _batchSize = batchSize;
            _cachedItems = new List<T>();
            _lastLoadedPage = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var i = 0;
            T item;
            while ((item = GetItem(i++)) != null)
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

        public int Count
        {
            get
            {
                if (!IsFullyLoaded)
                    Preload();
                return _cachedItems.Count;
            }
        }

        public bool IsFullyLoaded
        {
            get { return _fullyLoaded; }
        }

        protected void LoadNextPage()
        {
            var list = _func(++_lastLoadedPage, _batchSize);
            
            if (list.Any())
                _cachedItems.AddRange(list);
            else
                _fullyLoaded = true;
        }

        public T GetItem(int i)
        {
            while (true)
            {
                if (i < _cachedItems.Count)
                    return _cachedItems[i];

                if (_fullyLoaded)
                    return null;

                LoadNextPage();
            }
        }
    }

    public class ActiveItemsEnumerable : IEnumerable<ActiveItem>
    {
        private readonly IService _service;
        private readonly PaginatedEnumerable<ItemType> _itemEnum;

        public ActiveItemsEnumerable(IService service, int batchSize = 200)
        {
            _service = service;
            _itemEnum = new PaginatedEnumerable<ItemType>((p, n) => Enumerable.Cast<ItemType>(_service.GetActiveList(p, n)), batchSize);
        }

        public int Count
        {
            get { return _itemEnum.Count; }
        }
        
        public void Preload()
        {
            _itemEnum.Preload();
        }

        public IEnumerator<ActiveItem> GetEnumerator()
        {
            return _itemEnum.Select(item => new ActiveItem(item)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ActiveItem Get(int i)
        {
            return new ActiveItem(_itemEnum.GetItem(i));
        }
    }
}