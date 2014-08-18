using System.Collections.Generic;
using System.Linq;

namespace EbayTools.MaintenanceStrategies
{
    public class VariationItemShouldBe : ItemShouldBe
    {
        public VariationItemShouldBe()
        {
            Variations = new List<VariationShouldBe>();
        }

        public List<VariationShouldBe> Variations { get; private set; }

        public VariationShouldBe GetVariation(string sku)
        {
            return Variations.FirstOrDefault(v => v.SKU == sku);
        }
    }
}