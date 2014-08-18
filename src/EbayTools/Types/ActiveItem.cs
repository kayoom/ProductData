using System.Collections.Generic;
using System.Linq;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public class ActiveItem
    {
        public ActiveItem(ItemType item)
        {
            Item = item;
        }

        public ItemType Item { get; private set; }

        public IEnumerable<VariationType> Variations
        {
            get
            {
                return Item.Variations == null
                    ? Enumerable.Empty<VariationType>()
                    : Item.Variations.Variation.OfType<VariationType>();
            }
        }

        public bool IsVariation()
        {
            return Variations.Any();
        }

        public IEnumerable<StockableItem> StockableItems()
        {
            if (IsVariation())
            {
                return Variations.Select(v => new StockableVariation(Item, v));
            }
            return new List<StockableItem> {new StockableItem(Item)};
        }
    }
}