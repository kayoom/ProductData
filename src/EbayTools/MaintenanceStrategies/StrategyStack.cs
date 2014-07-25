using System.Collections.Generic;
using System.Linq;

namespace EbayTools.MaintenanceStrategies
{
    public class StrategyStack : List<IStrategy>, IStrategy
    {
        public bool IsApplicable(ItemShouldBe item)
        {
            return this.Any(strategy => strategy.IsApplicable(item));
        }

        public IEnumerable<ItemAction> Apply(ItemShouldBe item)
        {
            var strategy = this.FirstOrDefault(s => s.IsApplicable(item));
            return strategy == null ? Enumerable.Empty<ItemAction>() : strategy.Apply(item);
        }
    }
}