using System.Collections.Generic;
using System.Linq;
using MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class VariationItemUpdateQuantityStrategy : AbstractStrategy
    {
        private readonly int _limit;

        private class VariationDetail
        {
            public SKUDetailsType SKUDetail { get; private set; }
            public MerchantDataVariationType Variation { get; private set; }

            public VariationDetail(SKUDetailsType skuDetail, MerchantDataVariationType variation)
            {
                SKUDetail = skuDetail;
                Variation = variation;
            }
        }

        public VariationItemUpdateQuantityStrategy(IEnumerable<SKUDetailsType> skuDetails, int limit) : base(skuDetails)
        {
            _limit = limit;
        }

        public override bool IsApplicable(ItemShouldBe item)
        {
            var itemShouldBe = item as VariationItemShouldBe;
            return itemShouldBe != null && itemShouldBe.Variations.Any(v => v.Quantity != 0) && ApplicableVariations(itemShouldBe).Any();
        }

        private IEnumerable<VariationDetail> ApplicableVariations(VariationItemShouldBe itemShouldBe)
        {
            var itemsIs = GetItemIs(itemShouldBe.SKU).Where(item => item.Variations != null);
            return from itemIs in itemsIs
                   from variationIs in itemIs.Variations
                   let variationShouldBe = itemShouldBe.GetVariation(variationIs.SKU)
                   where variationShouldBe != null
                   where variationShouldBe.Quantity != variationIs.Quantity
                   where (variationShouldBe.Quantity < _limit || variationIs.Quantity < _limit)
                   select new VariationDetail(itemIs, variationIs);
        }

        public override IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var itemShouldBe = (VariationItemShouldBe) item;
            var variationsIs = ApplicableVariations(itemShouldBe).ToList();

            return variationsIs.Select(variationIs => new ItemAction(variationIs.SKUDetail.ItemID, new ReviseInventoryStatusRequestType
            {
                InventoryStatus = new[]
                {
                    new InventoryStatusType
                    {
                        ItemID = variationIs.SKUDetail.ItemID,
                        SKU = variationIs.Variation.SKU,
                        Quantity = itemShouldBe.GetVariation(variationIs.Variation.SKU).Quantity,
                        QuantitySpecified = true
                    }
                }
            }));
        }
    }
}