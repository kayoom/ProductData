using System;
using System.Collections.Generic;
using eBay.Service.Core.Soap;

namespace EbayTools
{
    public interface IService
    {
        ItemTypeCollection GetActiveList(int page = 1, int perPage = 200);
        ItemTypeCollection GetSellerList(DateTime from, DateTime to);
        ItemTypeCollection GetSellerList(int daysBeforeToday, int daysRange);
        SellingManagerProductTypeCollection GetSMInventory(int page = 1, int perPage = 200);
        SellingManagerTemplateDetailsTypeCollection GetSMTemplates(IEnumerable<long> templateIDs);
    }
}