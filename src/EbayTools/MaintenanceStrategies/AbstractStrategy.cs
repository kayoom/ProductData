using System.Collections.Generic;
using System.Linq;
using EbayLMS.MDS;

namespace EbayTools.MaintenanceStrategies
{
    public abstract class AbstractStrategy : IStrategy
    {
        protected AbstractStrategy(IEnumerable<SKUDetailsType> skuDetails)
        {
            SKUDetails = skuDetails.ToArray();
            SKUDetailsLookup = SKUDetails.ToLookup(s => s.SKU);
        }

        public SKUDetailsType[] SKUDetails { get; private set; }
        public ILookup<string, SKUDetailsType> SKUDetailsLookup { get; private set; }
        public abstract bool IsApplicable(ItemShouldBe item);
        public abstract IEnumerable<ItemAction> Apply(ItemShouldBe item);

        protected virtual IEnumerable<SKUDetailsType> GetItemIs(string sku)
        {
            return !SKUDetailsLookup.Contains(sku) ? new SKUDetailsType[0] : SKUDetailsLookup[sku];
        }
    }
}