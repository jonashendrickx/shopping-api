using System;
using JonasHendrickx.Shop.Contracts.Discounts;

namespace JonasHendrickx.Shop.Services.Discounts
{
    /// <summary>
    /// Buy an X amount of items and get Y free.
    /// </summary>
    public class BuyToFreeQuantityDiscountStrategy : BaseDiscountStrategy
    {
        public BuyToFreeQuantityDiscountStrategy(string input) : base(input)
        {
            Code = "BUY_TO_FREE_QTY";
        }

        public override CalculateInputModel Calculate(CalculateInputModel criteria)
        {
            var buyQuantity = Convert.ToUInt32(Input["buy_qty"]);
            var freeQuantity = Convert.ToUInt32(Input["free_qty"]);

            if (criteria.Quantity < buyQuantity)
            {
                return criteria;
            }
            
            // 2 buy - 1 free
            // 3
            var freeItems = criteria.Quantity / (buyQuantity + freeQuantity);
            criteria.TotalPrice = criteria.UnitPrice * (criteria.Quantity - freeItems);
            
            return criteria;
        }
    }
}