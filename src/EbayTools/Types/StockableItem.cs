using System.Linq;
using DotNetLibraries;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public class StockableItem
    {
        public StockableItem(ItemType item)
        {
            Item = item;
        }

        public ItemType Item { get; private set; }

        public virtual string ID
        {
            get { return Item.ItemID; }
        }

        public virtual string SKU
        {
            get { return Item.SKU; }
        }

        public virtual int VariationDims
        {
            get { return 0; }
        }

        public virtual MoneyAmount Price
        {
            get { return Item.SellingStatus.CurrentPrice.ToMoneyAmount(); }
        }

        public virtual MoneyAmount ShippingCost
        {
            get
            {
                var shippingServiceOptions =
                    Item.ShippingDetails.ShippingServiceOptions.Cast<ShippingServiceOptionsType>().FirstOrDefault();
                return shippingServiceOptions != null
                    ? shippingServiceOptions.ShippingServiceCost.ToMoneyAmount()
                    : MoneyAmount.Zero;
            }
        }

        public virtual string ItemSKU
        {
            get { return Item.SKU; }
        }

        public virtual int Quantity
        {
            get { return Item.Quantity - Item.SellingStatus.QuantitySold; }
        }
    }
}