using DotNetLibraries;
using eBay.Service.Core.Soap;

namespace EbayTools.Types
{
    public static class AmountTypeExtension
    {
        public static MoneyAmount ToMoneyAmount(this AmountType amount)
        {
            return amount == null ? MoneyAmount.Zero : new MoneyAmount(amount.Value, amount.currencyID.ToString());
        }
    }
}