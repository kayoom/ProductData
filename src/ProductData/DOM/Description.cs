using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Description
    {
        [XmlAttribute("lang", DataType = "token")]
        public string LangCode { get; set; }

        [XmlAttribute("type")]
        public DescriptionType Type { get; set; }

        [XmlText]
        public string Content { get; set; }
    }
}