using System.Collections.Generic;
using System.Linq;
using EbayLMS.MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class SimpleItemCancelStrategy : AbstractStrategy
    {
        public SimpleItemCancelStrategy(IEnumerable<SKUDetailsType> skuDetails) : base(skuDetails)
        {
        }

        public override bool IsApplicable(ItemShouldBe item)
        {
            var simpleItem = item as SimpleItemShouldBe;
            return simpleItem != null && simpleItem.Quantity == 0 && GetItemIs(simpleItem.SKU).Any();
        }

        public override IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var simpleItem = (SimpleItemShouldBe) item;
            var skuDetails = GetItemIs(simpleItem.SKU);

            return skuDetails.Select(d => new ItemAction(d.ItemID, new EndFixedPriceItemRequestType
            {
                ItemID = d.ItemID,
                EndingReason = EndReasonCodeType.NotAvailable,
                EndingReasonSpecified = true
            }));
        }
    }
}