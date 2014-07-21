using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class VariantImage : IIdentifiable
    {
        public VariantImage()
        {
            Images = new List<Ref>();
        }

        [XmlAttribute("value_id", DataType = "token")]
        public string ValueID { get; set; }

        [XmlArrayItem("Image")]
        public List<Ref> Images { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}