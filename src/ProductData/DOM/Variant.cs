using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Variant : IIdentifiable
    {
        public Variant()
        {
            VariationValues = new List<VariationValue>();
        }

        public List<VariationValue> VariationValues { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}