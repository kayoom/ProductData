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
            get { return MakeID(base.ID, Variation.SKU); }
        }

        public override string SKU
        {
            get { return Variation.SKU; }
        }

        public override MoneyAmount Price
        {
            get { return Variation.StartPrice.ToMoneyAmount(); }
        }

        public override int Quantity
        {
            get { return Variation.Quantity - Variation.SellingStatus.QuantitySold; }
        }

        public override int VariationDims
        {
            get { return Variation.VariationSpecifics.Count; }
        }

        public static string MakeID(string id, string variationSKU)
        {
            if (string.IsNullOrWhiteSpace(variationSKU))
                return null;
            var sku = variationSKU.Trim();
            var itemID = id + "-";
            var n = sku.Length - 30 + itemID.Length;
            if (n < 0)
                n = 0;
            return itemID + sku.Substring(n);
        }
    }
}