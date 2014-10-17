using System.Xml.Serialization;

namespace ProductData.DOM
{
    public enum DescriptionType
    {
        [XmlEnum("text")] Text,
        [XmlEnum("html")] HTML,
        [XmlEnum("markdown")] Markdown
    }
}