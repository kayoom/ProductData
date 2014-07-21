using System.Collections.Generic;
using System.Linq;
using ProductData;
using ProductData.DOM;

namespace ProductDataRendering
{
    public class Product : IProduct
    {
        private readonly ProductData.DOM.Product _domProduct;
        private readonly Catalog _domCatalog;
        private readonly string _langCode;

        public Product(ProductData.DOM.Product domProduct, ProductData.DOM.Catalog domCatalog, string langCode)
        {
            _domProduct = domProduct;
            _domCatalog = domCatalog;
            _langCode = langCode;
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

        public string ID
        {
            get { return _domProduct.ID; }
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