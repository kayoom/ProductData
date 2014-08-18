using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Attribute : IIdentifiable
    {
        public Attribute()
        {
            Names = new List<Name>();
            Values = new List<Value>();
        }

        public List<Name> Names { get; set; }
        public List<Value> Values { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}