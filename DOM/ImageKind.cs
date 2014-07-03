using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class ImageKind : IIdentifiable
    {
        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}