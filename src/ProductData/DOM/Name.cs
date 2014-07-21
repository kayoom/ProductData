using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Name
    {
        [XmlText]
        public string Content { get; set; }

        [XmlAttribute("lang", DataType = "token")]
        public string LangCode { get; set; }
    }
}