using System.Xml.Serialization;

namespace ProductData.DOM
{
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