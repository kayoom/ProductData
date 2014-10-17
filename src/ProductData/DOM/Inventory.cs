using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Inventory
    {
        [XmlElement("InventoryItem")]
        public List<InventoryItem> InventoryItems { get; set; }

        public Inventory()
        {
            InventoryItems = new List<InventoryItem>();
        }
    }
}