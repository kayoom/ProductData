using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Value : IIdentifiable
    {
        public Value()
        {
            Names = new List<Name>();
        }

        public List<Name> Names { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}