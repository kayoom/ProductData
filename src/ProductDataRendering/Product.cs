using System.Collections.Generic;
using System.Linq;
using DotNetLibraries;
using ProductData;
using ProductData.DOM;

namespace ProductDataRendering
{
    public class Product : IProduct
    {
        private readonly ProductData.DOM.Product _domProduct;
        private readonly Catalog _domCatalog;
        private readonly string _langCode;
        private readonly string _currency;

        public Product(ProductData.DOM.Product domProduct, Catalog domCatalog, string langCode, string currency)
        {
            _domProduct = domProduct;
            _domCatalog = domCatalog;
            _langCode = langCode;
            _currency = currency;
        }

        public string Name
        {
            get { return _domProduct.Names.Get(_langCode); }
        }

        public string Description
        {
            get { return _domProduct.Descriptions.Get(_langCode); }
        }

        public Dictionary<string, string> Properties
        {
            get { return GetProperties(); }
        }

        public List<string> SellingPoints
        {
            get { return GetSellingPoints(); }
        }

        public string MainImageURL
        {
            get { return GetMainImageURL(); }
        }

        public IEnumerable<string> ImageURLs
        {
            get { return GetImageURLs(); }
        }

        private IEnumerable<string> GetImageURLs()
        {
            return _domProduct.Images.Select(imageRef => _domCatalog.Images.First(i => i.ID == imageRef.ID)).Select(image => image.URL);
        }

        public decimal Price
        {
            get { return GetPrice(); }
        }

        public IEnumerable<Variant> Variants
        {
            get { return _domProduct.Variants.Select(v => new Variant(v, _domProduct, _domCatalog, GetItem(v.ID), _langCode)).OrderBy(v => v.Item.Price); }
        }

        public Item Item
        {
            get { return GetItems().First(); }
        }

        private Item GetItem(string id)
        {
            return GetItems().First(i => i.ID == id);
        }

        private decimal GetPrice()
        {
            return GetPrices().Min();
        }

        public IEnumerable<decimal> GetPrices()
        {
            return GetItems().Select(i => i.Price);
        }

        public IEnumerable<Item> GetItems()
        {
            if (_domProduct.Item != null)
                return new[]
                {new Item(_domCatalog.Items.First(i => i.ID == _domProduct.Item.ID), _domCatalog, _currency)};

            return _domProduct.Variants.Select(
                    v => new Item(_domCatalog.Items.First(i => i.ID == v.ID), _domCatalog, _currency));
        } 

        public string ID
        {
            get { return _domProduct.ID; }
        }

        public string Currency
        {
            get { return _currency; }
        }

        private string GetMainImageURL()
        {
            var firstImageRef = _domProduct.Images.FirstOrDefault();
            if (firstImageRef == null)
                return "";
            var image = _domCatalog.Images.First(i => i.ID == firstImageRef.ID);
            return image.URL;
        }

        private List<string> GetSellingPoints()
        {
            return _domProduct.SellingPoints.
                Select(pointRef => pointRef.ID).
                Select(pointID => _domCatalog.Definition.SellingPoints.First(s => s.ID == pointID)).
                Select(point => point.Names.Get(_langCode)).
                Where(pointName => !string.IsNullOrWhiteSpace(pointName)).
                ToList();
        }

        private Dictionary<string, string> GetProperties()
        {
            var retVal = new Dictionary<string, string>();

            foreach (var attrRef in _domProduct.Attributes)
            {
                var keyID = attrRef.ID;
                var valueID = attrRef.ValueID;
                var attr = _domCatalog.Definition.Attributes.First(a => a.ID == keyID);
                var keyName = attr.Names.Get(_langCode);
                var value = attr.Values.First(v => v.ID == valueID);
                var valueName = value.Names.Get(_langCode);

                if(string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(valueName))
                    continue;

                retVal[keyName] = valueName;
            }

            return retVal;
        }
    }
}