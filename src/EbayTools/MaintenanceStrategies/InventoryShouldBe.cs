using System.Collections.Generic;

namespace EbayTools.MaintenanceStrategies
{
    public class InventoryShouldBe
    {
        public List<ItemShouldBe> Items { get; private set; }

        public InventoryShouldBe()
        {
            Items = new List<ItemShouldBe>();
        }
    }
}