using System.Collections.Generic;
using System.Linq;
using MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class VariationItemAddStrategy : AbstractStrategy
    {
        private readonly IEbayPricelist _pricelist;

        public VariationItemAddStrategy(IEnumerable<SKUDetailsType> skuDetails, IEbayPricelist pricelist)
            : base(skuDetails)
        {
            _pricelist = pricelist;
        }

        public override bool IsApplicable(ItemShouldBe item)
        {
            var itemShouldBe = item as VariationItemShouldBe;
            return itemShouldBe != null && ApplicableItems(itemShouldBe).Any();
        }

        private IEnumerable<SKUDetailsType> ApplicableItems(VariationItemShouldBe itemShouldBe)
        {
            return GetItemIs(itemShouldBe.SKU).Where(itemIs => GetMissingSKUs(itemShouldBe, itemIs).Any());
        }

        private static IEnumerable<string> GetMissingSKUs(VariationItemShouldBe itemShouldBe, SKUDetailsType itemIs)
        {
            return itemShouldBe.Variations.Select(v => v.SKU).Except(itemIs.Variations.Select(v => v.SKU));
        }

        public override IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var itemShouldBe = (VariationItemShouldBe) item;
            var itemsIs = ApplicableItems(itemShouldBe);

            return itemsIs.Select(itemIs => new ItemAction(itemIs.ItemID, MakeRequest(itemIs, itemShouldBe)));
        }

        private IEnumerable<AbstractRequestType> MakeRequest(SKUDetailsType itemIs, VariationItemShouldBe itemShouldBe)
        {
            yield return new ReviseFixedPriceItemRequestType
            {
                Item = new ItemType
                {
                    ItemID = itemIs.ItemID,
                    Variations = new VariationsType
                    {
                        VariationSpecificsSet = GetVariationSpecificsSet(itemShouldBe).ToArray(),
                        Variation = GetVariations(itemShouldBe).ToArray()
                    }
                }
            };
        }

        private IEnumerable<VariationType> GetVariations(VariationItemShouldBe itemShouldBe)
        {
            return
                itemShouldBe.Variations.Select(variationShouldBe => MakeVariation(itemShouldBe, variationShouldBe))
                    .Where(v => v != null);
        }

        private VariationType MakeVariation(VariationItemShouldBe itemShouldBe, VariationShouldBe variationShouldBe)
        {
            var price = GetPrice(itemShouldBe, variationShouldBe);
            if (!price.HasValue)
                return null;

            return new VariationType
            {
                SKU = variationShouldBe.SKU,
                Quantity = variationShouldBe.Quantity,
                QuantitySpecified = true,
                StartPrice = new AmountType
                {
                    currencyID = CurrencyCodeType.EUR,
                    Value = price.Value
                },
                VariationSpecifics = GetVariationSpecifics(variationShouldBe).ToArray()
            };
        }

        private double? GetPrice(VariationItemShouldBe itemShouldBe, VariationShouldBe variationShouldBe)
        {
            return _pricelist.GetPrice(itemShouldBe.SKU, variationShouldBe.SKU, "EUR");
        }

        private IEnumerable<NameValueListType> GetVariationSpecifics(VariationShouldBe variationShouldBe)
        {
            return variationShouldBe.Specifics.Select(kv => new NameValueListType
            {
                Name = kv.Key,
                Value = new[] {kv.Value}
            });
        }

        private IEnumerable<NameValueListType> GetVariationSpecificsSet(VariationItemShouldBe itemShouldBe)
        {
            return
                itemShouldBe.Variations.SelectMany(v => v.Specifics)
                    .ToLookup(kv => kv.Key, kv => kv.Value)
                    .Select(g => new NameValueListType
                    {
                        Name = g.Key,
                        Value = g.ToArray()
                    });
        }
    }
}