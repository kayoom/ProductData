using System.Collections.Generic;

namespace ProductData.DOM
{
    public class Definition
    {
        public Definition()
        {
            VariationDimensions = new List<VariationDimension>();
            Attributes = new List<Attribute>();
            SellingPoints = new List<SellingPoint>();
            Flags = new List<Flag>();
            Tags = new List<Tag>();
            ShippingTypes = new List<ShippingType>();
            Languages = new List<Language>();
            ImageKinds = new List<ImageKind>();
        }

        public List<Language> Languages { get; set; }
        public List<ShippingType> ShippingTypes { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Flag> Flags { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<SellingPoint> SellingPoints { get; set; }
        public List<ImageKind> ImageKinds { get; set; }
        public List<VariationDimension> VariationDimensions { get; set; }
    }
}