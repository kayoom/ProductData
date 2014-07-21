using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Image : IIdentifiable
    {
        [XmlAttribute("type")]
        public ImageType Type { get; set; }

        [XmlAttribute("kind_id", DataType = "token")]
        public string KindID { get; set; }

        [XmlAttribute("url", DataType = "anyURI")]
        public string URL { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}