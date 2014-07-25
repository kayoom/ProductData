using System.Collections.Generic;

namespace EbayTools.MaintenanceStrategies
{
    public class VariationShouldBe
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }

        public Dictionary<string, string> Specifics { get; private set; }

        public VariationShouldBe()
        {
            Specifics = new Dictionary<string, string>();
        }
    }
}