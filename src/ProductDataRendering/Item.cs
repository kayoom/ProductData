using System.Linq;
using ProductData.DOM;

namespace ProductDataRendering
{
    public class Item
    {
        private readonly ProductData.DOM.Item _domItem;
        private readonly Catalog _domCatalog;
        private readonly string _currency;

        public Item(ProductData.DOM.Item domItem, Catalog domCatalog, string currency)
        {
            _domItem = domItem;
            _domCatalog = domCatalog;
            _currency = currency;
        }

        public decimal Price
        {
            get { return _domItem.Prices.Where(p => p.Currency == _currency).Select(p => p.SellingPrice).FirstOrDefault(); }
        }

        public string Currency
        {
            get { return _currency; }
        }

        public string ID
        {
            get { return _domItem.ID; }
        }
    }
}