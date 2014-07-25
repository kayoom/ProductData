using System.Collections.Generic;
using System.Linq;
using ProductData;
using ProductData.DOM;

namespace ProductDataRendering
{
    public class Variant
    {
        private readonly ProductData.DOM.Variant _domVariant;
        private readonly ProductData.DOM.Product _domProduct;
        private readonly Catalog _domCatalog;
        private readonly Item _item;
        private readonly string _langCode;

        public Variant(ProductData.DOM.Variant domVariant, ProductData.DOM.Product domProduct, Catalog domCatalog, Item item, string langCode)
        {
            _domVariant = domVariant;
            _domProduct = domProduct;
            _domCatalog = domCatalog;
            _item = item;
            _langCode = langCode;
        }

        public Item Item
        {
            get { return _item; }
        }

        public Dictionary<string, string> Values
        {
            get { return GetVariationValues(); }
        }

        public string Name
        {
            get { return string.Join(" ", Values.Select(v => v.Key)); }
        }

        public IEnumerable<string> ImageURLs
        {
            get { return GetImageURLs(); }
        }

        private IEnumerable<string> GetImageURLs()
        {
            return
                _domProduct.VariantImages.Where(
                    vi => _domVariant.VariationValues.Any(v => v.ID == vi.ID && v.ValueID == vi.ValueID))
                    .SelectMany(v => v.Images)
                    .Select(i => _domCatalog.Images.First(im => im.ID == i.ID).URL);
        }

        private Dictionary<string, string> GetVariationValues()
        {
            var retVal = new Dictionary<string, string>();

            foreach (var variationValue in _domVariant.VariationValues)
            {
                var keyID = variationValue.ID;
                var valueID = variationValue.ValueID;
                var key = _domCatalog.Definition.VariationDimensions.First(d => d.ID == keyID);
                var value = key.Values.First(v => v.ID == valueID);

                retVal[key.Names.Get(_langCode)] = value.Names.Get(_langCode);
            }

            return retVal;
        }
    }
}