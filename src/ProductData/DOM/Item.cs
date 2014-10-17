using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Item : IIdentifiable
    {
        public Item()
        {
            Prices = new List<Price>();
            Availability = new Availability();
            Dimensions = new Dimensions();
            Shipping = new Shipping();
        }

        public string EAN13 { get; set; }

        public List<Price> Prices { get; set; }
        public Availability Availability { get; set; }
        public Dimensions Dimensions { get; set; }
        public Shipping Shipping { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}