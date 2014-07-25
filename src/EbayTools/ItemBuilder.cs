using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using eBay.Service.Core.Soap;
using ProductDataRendering;

namespace EbayTools
{
    public class ItemBuilder
    {
        private readonly ItemType _template;
        private readonly IImageService _imageService;
        private readonly ISpecificsMatcher _specificsMatcher;
        private readonly XmlSerializer _serializer;
        private readonly MemoryStream _stream;

        public ItemBuilder(ItemType template, IImageService imageService = null, ISpecificsMatcher specificsMatcher = null)
        {
            _serializer = new XmlSerializer(typeof(ItemType));
            _stream = new MemoryStream();
            _template = template;
            _imageService = imageService ?? new DummyImageService();
            _specificsMatcher = specificsMatcher ?? new DummySpecificsMatcher();
            _serializer.Serialize(_stream, _template);
        }

        public ItemType Build(Product product, string body = null, string sku = null)
        {
            sku = sku ?? product.ID;
            body = body ?? product.Description;

            var item = CloneTemplate();

            item.SKU = sku;

            item.Title = product.Name;
            if (item.Title.Length > 80)
                item.Title = item.Title.Substring(0, 80);
            item.Description = body;

            item.LookupAttributeArray = null;

            item.ReservePrice = null;
            item.BuyItNowPrice = null;

            item.ShippingDetails.CalculatedShippingRate = null;

            var pictureURLs = GetPictureURLs(product);
            item.PictureDetails.GalleryURL = pictureURLs.First();
            item.PictureDetails.PictureURL = new StringCollection(pictureURLs);

            if (product.Variants.Any())
                BuildVariants(item, product);
            else
                BuildItem(item, product);

            BuildItemSpecifics(item, product);  //TODO: specifics matching
            return item;
        }

        private string[] GetPictureURLs(Product product)
        {
            return product.ImageURLs.Select(url => _imageService.GetURL(new Uri(url), new ImageFormat()).ToString()).ToArray();
        }

        private void BuildItemSpecifics(ItemType item, Product product)
        {
            foreach (var property in product.Properties)
            {
                item.ItemSpecifics.Add(new NameValueListType
                {
                    Name = _specificsMatcher.GetKey(property.Key),
                    Value = new StringCollection {_specificsMatcher.GetValue(property.Key, property.Value)}
                });
            }
        }

        private void BuildItem(ItemType item, Product product)
        {
            if (item.Variations != null)
                item.Variations = null;

            item.Currency = GetCurrency(product.Currency);
            item.StartPrice = new AmountType
            {
                currencyID = item.Currency,
                Value = (double) product.Price
            };
        }

        private void BuildVariants(ItemType item, Product product)
        {
            item.Variations.Variation.Clear();
            item.Variations.VariationSpecificsSet.Clear();

            var values = product.Variants.SelectMany(v => v.Values).ToLookup(kv => kv.Key, kv => kv.Value);
            foreach (var value in values)
            {
                item.Variations.VariationSpecificsSet.Add(new NameValueListType
                {
                    Name = value.Key,
                    Value = new StringCollection(value.ToArray())
                });
            }

            foreach (var variant in product.Variants)
            {
                item.Variations.Variation.Add(new VariationType
                {
                    Quantity = 5,
                    QuantitySpecified = true,
                    SKU = variant.Item.ID,
                    StartPrice = new AmountType
                    {
                        currencyID = GetCurrency(variant.Item.Currency),
                        Value = (double) variant.Item.Price
                    },
                    VariationTitle = variant.Name,
                    VariationSpecifics = GetVariationSpecifics(variant)
                });
            }
        }

        private NameValueListTypeCollection GetVariationSpecifics(Variant variant)
        {
            var collection = new NameValueListTypeCollection();
            foreach (var value in variant.Values)
            {
                collection.Add(new NameValueListType
                {
                    Name = value.Key,
                    Value = new StringCollection(new[] {value.Value})
                });
            }
            return collection;
        }

        private CurrencyCodeType GetCurrency(string currency)
        {
            if(currency == "EUR")
                return CurrencyCodeType.EUR;
            throw new ArgumentException("Unknown currency: " + currency, "currency");
        }

        private ItemType CloneTemplate()
        {
            _stream.Seek(0, SeekOrigin.Begin);

            return (ItemType) _serializer.Deserialize(_stream);
        }
    }

    public interface ISpecificsMatcher
    {
        string GetKey(string key);
        string GetValue(string key, string value);
    }

    public class DummySpecificsMatcher : ISpecificsMatcher
    {
        public string GetKey(string key)
        {
            return key;
        }

        public string GetValue(string key, string value)
        {
            return value;
        }
    }
}
