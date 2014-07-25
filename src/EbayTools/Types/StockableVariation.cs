using DotNetLibraries;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public class StockableVariation : StockableItem
    {
        public StockableVariation(ItemType item, VariationType variation) : base(item)
        {
            Variation = variation;
        }

        public VariationType Variation { get; private set; }

        public override string ID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Variation.SKU))
                    return null;
                var sku = Variation.SKU.Trim();
                var itemID = base.ID + "-";
                var n = sku.Length - 30 + itemID.Length;
                if (n < 0)
                    n = 0;
                return itemID + sku.Substring(n);
            }
        }

        public override string SKU
        {
            get { return Variation.SKU; }
        }

        public override MoneyAmount Price
        {
            get { return Variation.StartPrice.ToMoneyAmount(); }
        }

        public override int VariationDims
        {
            get { return Variation.VariationSpecifics.Count; }
        }
    }
}