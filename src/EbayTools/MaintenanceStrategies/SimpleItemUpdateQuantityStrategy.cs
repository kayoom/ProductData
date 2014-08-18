using System.Collections.Generic;
using System.Linq;
using EbayLMS.MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class SimpleItemUpdateQuantityStrategy : AbstractStrategy
    {
        private readonly int _limit;

        public SimpleItemUpdateQuantityStrategy(IEnumerable<SKUDetailsType> skuDetails, int limit) : base(skuDetails)
        {
            _limit = limit;
        }

        public override bool IsApplicable(ItemShouldBe item)
        {
            var itemShouldBe = item as SimpleItemShouldBe;
            return itemShouldBe != null && itemShouldBe.Quantity > 0 && GetItemsIs(itemShouldBe).Any();
        }

        public override IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var itemShouldBe = (SimpleItemShouldBe) item;
            var itemsIs = GetItemsIs(itemShouldBe);

            return itemsIs.Select(itemIs => new ItemAction(itemIs.ItemID, new ReviseInventoryStatusRequestType
            {
                InventoryStatus = new[]
                {
                    new InventoryStatusType
                    {
                        ItemID = itemIs.ItemID,
                        Quantity = itemShouldBe.Quantity,
                        QuantitySpecified = true
                    }
                }
            }));
        }

        private IEnumerable<SKUDetailsType> GetItemsIs(SimpleItemShouldBe simpleItem)
        {
            return
                GetItemIs(simpleItem.SKU)
                    .Where(
                        d => d.Quantity != simpleItem.Quantity && (d.Quantity < _limit || simpleItem.Quantity < _limit));
        }
    }
}