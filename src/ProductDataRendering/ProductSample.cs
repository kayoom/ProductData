using System.Collections.Generic;

namespace ProductDataRendering
{
    public class ProductSample : IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<string> SellingPoints { get; set; }
        public string MainImageURL { get; set; }
        public string ID { get; set; }
    }
}