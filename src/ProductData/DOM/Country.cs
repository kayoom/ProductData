using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Country
    {
        [XmlText]
        public string Code { get; set; }
    }
}