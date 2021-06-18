using System;
using JonasHendrickx.Shop.Contracts.Discounts;

namespace JonasHendrickx.Shop.Services.Discounts
{
    /// <summary>
    /// Buy a minimum quantity of items to get a discount of #.##%.
    /// </summary>
    public class QuantityToPercentageDiscountStrategy : BaseDiscountStrategy
    {
        public QuantityToPercentageDiscountStrategy(string input) : base(input)
        {
            Code = "QTY_TO_PCT";
        }

        public override CalculateInputModel Calculate(CalculateInputModel criteria)
        {
            var minQuantity = Convert.ToUInt32(Input["qty"]);
            var percentage = Convert.ToDecimal(Input["pct"]);

            if (criteria.Quantity < minQuantity)
            {
                return criteria;
            }

            criteria.TotalPrice *= 1 - percentage;
            criteria.UnitPrice *= 1 - percentage;
            
            return criteria;
        }
    }
}