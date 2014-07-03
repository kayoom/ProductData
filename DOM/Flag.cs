using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Flag : IIdentifiable
    {
        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}