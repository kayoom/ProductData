namespace EbayTools.MaintenanceStrategies
{
    public interface IEbayPricelist
    {
        double? GetPrice(string itemSKU, string variationSKU, string currency);
    }

}