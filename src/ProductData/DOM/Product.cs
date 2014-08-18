using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    public class Product : IIdentifiable
    {
        public Product()
        {
            Names = new List<Name>();
            Descriptions = new List<Description>();
            Variants = new List<Variant>();
            VariantImages = new List<VariantImage>();
            Images = new List<Ref>();
            SellingPoints = new List<Ref>();
            Attributes = new List<AttributeValueRef>();
            Tags = new List<Ref>();
            Flags = new List<Ref>();
        }

        public Ref Item { get; set; }
        public List<Variant> Variants { get; set; }

        [XmlIgnore]
        public bool VariantsSpecified
        {
            get { return !ItemSpecified; }
        }

        [XmlIgnore]
        public bool ItemSpecified
        {
            get { return Item != null; }
        }

        public List<VariantImage> VariantImages { get; set; }
        public List<Name> Names { get; set; }
        public List<Description> Descriptions { get; set; }

        [XmlArrayItem("Tag")]
        public List<Ref> Tags { get; set; }

        [XmlArrayItem("Flag")]
        public List<Ref> Flags { get; set; }

        [XmlArrayItem("Attribute")]
        public List<AttributeValueRef> Attributes { get; set; }

        [XmlArrayItem("SellingPoint")]
        public List<Ref> SellingPoints { get; set; }

        [XmlArrayItem("Image")]
        public List<Ref> Images { get; set; }

        [XmlAttribute("id", DataType = "token")]
        public string ID { get; set; }
    }
}