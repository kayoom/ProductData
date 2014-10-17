using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Shipping
    {
        public Shipping()
        {
            ShippingTypes = new List<Ref>();
            ShippingDimensions = new Dimensions();
        }

        [XmlArrayItem("ShippingType")]
        public List<Ref> ShippingTypes { get; set; }

        public Dimensions ShippingDimensions { get; set; }
    }
}