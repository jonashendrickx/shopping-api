using System;
using JonasHendrickx.Shop.Contracts.Discounts;

namespace JonasHendrickx.Shop.Services.Discounts
{
    public class QuantityToPercentageDiscountStrategy : BaseDiscountStrategy
    {
        public QuantityToPercentageDiscountStrategy(string input) : base(input)
        {
            Code = "QUANTITY_TO_PERCENTAGE";
        }

        public override decimal Calculate(CalculateInputModel criteria)
        {
            var minQuantity = Convert.ToUInt32(Input["qty"]);
            var percentage = Convert.ToDecimal(Input["pct"]);

            if (criteria.Quantity < minQuantity)
            {
                return 0;
            }

            return criteria.TotalPrice * percentage;
        }
    }
}