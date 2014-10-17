using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class ShippingType : IIdentifiable
    {
        public ShippingType()
        {
            ShippingCosts = new List<ShippingCost>();
            Countries = new List<Country>();
        }

        public List<ShippingCost> ShippingCosts { get; set; }
        public List<Country> Countries { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}