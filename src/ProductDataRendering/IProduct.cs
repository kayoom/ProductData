using System.Collections.Generic;

namespace ProductDataRendering
{
    public interface IProduct
    {
        string Name { get; }
        string Description { get; }
        Dictionary<string, string> Properties { get; }
        List<string> SellingPoints { get; }
        string MainImageURL { get; }
        string ID { get; }
    }
}