using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ProductData.DOM
{
    [XmlRoot]
    public class Catalog
    {
        public Catalog()
        {
            Definition = new Definition();
            Items = new List<Item>();
            Products = new List<Product>();
            Images = new List<Image>();
        }

        public Definition Definition { get; set; }
        public List<Image> Images { get; set; }
        public List<Item> Items { get; set; }
        public List<Product> Products { get; set; }

        private static XmlSerializer Serializer
        {
            get { return new XmlSerializer(typeof (Catalog)); }
        }

        public void Save(Stream stream)
        {
            Serializer.Serialize(stream, this);
        }

        public void Save(TextWriter writer)
        {
            Serializer.Serialize(writer, this);
        }

        public void Save(string filename, Encoding encoding)
        {
            Serializer.Serialize(new StreamWriter(filename, false, encoding), this);
        }

        public static Catalog Load(Stream stream)
        {
            return (Catalog) Serializer.Deserialize(stream);
        }

        public static Catalog Load(TextReader reader)
        {
            return (Catalog) Serializer.Deserialize(reader);
        }

        public static Catalog Load(string filename)
        {
            return (Catalog) Serializer.Deserialize(new StreamReader(filename));
        }
    }
}