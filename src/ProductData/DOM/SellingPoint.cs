using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class SellingPoint : IIdentifiable
    {
        public SellingPoint()
        {
            Names = new List<Name>();
        }

        public List<Name> Names { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}