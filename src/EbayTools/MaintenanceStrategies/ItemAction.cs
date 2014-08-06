using System.Collections.Generic;
using System.Linq;
using EbayLMS.MDS;

namespace EbayTools.MaintenanceStrategies
{
    public class ItemAction
    {
        public string ItemID { get; private set; }
        public List<AbstractRequestType> Requests { get; private set; }

        public ItemAction(string itemID, IEnumerable<AbstractRequestType> requests = null)
        {
            ItemID = itemID;
            Requests = (requests ?? Enumerable.Empty<AbstractRequestType>()).ToList();
        }

        public ItemAction(string itemID, AbstractRequestType request)
        {
            ItemID = itemID;
            Requests = new List<AbstractRequestType>{request};
        }
    }
}