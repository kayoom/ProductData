using System.Collections.Generic;

namespace EbayTools.MaintenanceStrategies
{
    public class InventoryShouldBe
    {
        public InventoryShouldBe()
        {
            Items = new List<ItemShouldBe>();
        }

        public List<ItemShouldBe> Items { get; private set; }
    }
}