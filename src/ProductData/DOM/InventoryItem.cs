namespace ProductData.DOM
{
    public class InventoryItem : IIdentifiable
    {
        public string ID { get; set; }
        public Availability Availability { get; set; }

        public InventoryItem()
        {
            Availability = new Availability();
        }
    }
}