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

    public class Country
    {
        [XmlText]
        public string Code { get; set; }
    }

    public class ShippingCost
    {
        [XmlAttribute("currency", DataType = "token")]
        public string Currency { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }
}