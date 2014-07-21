using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Item : IIdentifiable
    {
        public Item()
        {
            Prices = new List<Price>();
            Availability = new Availability();
            Dimensions = new Dimensions();
            Shipping = new Shipping();
        }

        public string EAN13 { get; set; }

        public List<Price> Prices { get; set; }
        public Availability Availability { get; set; }
        public Dimensions Dimensions { get; set; }
        public Shipping Shipping { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }

    public class Availability
    {
        public Boolean Active { get; set; }
        public Boolean Replaceable { get; set; }
        public uint LeadTime { get; set; }
        public uint ReplaceTime { get; set; }
        public uint InStock { get; set; }
    }

    public class Dimensions
    {
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public decimal Volume { get; set; }
        public decimal Weight { get; set; }
    }

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

    public class Price
    {
        public Price()
        {
            SellingPrice = 9999999.99m;
        }

        [XmlAttribute("currency")]
        public string Currency { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal? SuggestedPrice { get; set; }

        [XmlIgnore]
        public bool SuggestedPriceSpecified
        {
            get { return SuggestedPrice.HasValue; }
            set
            {
                if (!value)
                    SuggestedPrice = null;
            }
        }

        public decimal? PreviousPrice { get; set; }

        [XmlIgnore]
        public bool PreviousPriceSpecified
        {
            get { return PreviousPrice.HasValue; }
            set
            {
                if (!value)
                    PreviousPrice = null;
            }
        }
    }
}