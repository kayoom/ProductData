using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class ShippingCost
    {
        [XmlAttribute("currency", DataType = "token")]
        public string Currency { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }
}