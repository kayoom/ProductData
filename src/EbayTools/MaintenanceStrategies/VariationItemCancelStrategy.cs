using System.Collections.Generic;
using System.Linq;
using MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class VariationItemCancelStrategy : AbstractStrategy
    {
        public VariationItemCancelStrategy(IEnumerable<SKUDetailsType> skuDetails) : base(skuDetails)
        {
        }

        public override bool IsApplicable(ItemShouldBe item)
        {
            var itemShouldBe = item as VariationItemShouldBe;
            if (itemShouldBe == null)
                return false;
            
            var itemsIs = GetItemIs(itemShouldBe.SKU);
            return itemsIs.Any() && itemShouldBe.Variations.All(v => v.Quantity == 0);
        }

        public override IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var itemShouldBe = (VariationItemShouldBe) item;
            var itemsIs = GetItemIs(itemShouldBe.SKU);

            return itemsIs.Select(i => new ItemAction(i.ItemID, new EndFixedPriceItemRequestType
            {
                ItemID = i.ItemID,
                EndingReason = EndReasonCodeType.NotAvailable,
                EndingReasonSpecified = true
            }));
        }
    }
}