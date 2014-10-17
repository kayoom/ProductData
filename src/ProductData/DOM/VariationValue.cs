using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class VariationValue : IIdentifiable
    {
        [XmlAttribute("value_id", DataType = "token")]
        public string ValueID { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}