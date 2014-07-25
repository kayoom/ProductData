using System.Collections.Generic;

namespace EbayTools.MaintenanceStrategies
{
    public interface IStrategy
    {
        bool IsApplicable(ItemShouldBe item);
        IEnumerable<ItemAction> Apply(ItemShouldBe item);
    }
}