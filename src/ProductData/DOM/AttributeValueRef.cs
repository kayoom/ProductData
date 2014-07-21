using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class AttributeValueRef : IIdentifiable
    {
        [XmlAttribute("value_id", DataType = "token")]
        public string ValueID { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}