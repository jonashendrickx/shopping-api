using System;
using System.Linq;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Contracts;
using JonasHendrickx.Shop.Contracts.Discounts;
using JonasHendrickx.Shop.Infrastructure.Contracts;
using JonasHendrickx.Shop.Services.Discounts;

namespace JonasHendrickx.Shop.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductListingRepository _productListingRepository;
        
        public BasketService(IBasketRepository basketRepository, IProductListingRepository productListingRepository)
    {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _productListingRepository = productListingRepository ?? throw new ArgumentNullException(nameof(basketRepository));
    }
        
        public async Task<Guid> CreateAsync()
        {
            var id = await _basketRepository.CreateAsync();
            return id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _basketRepository.DeleteAsync(id);
        }

        public async Task<decimal> GetAmountAsync(Guid id)
        {
            var basket = await _basketRepository.GetAsync(id);

            if (basket == null)
            {
                throw new ArgumentException("BASKET_NOT_FOUND");
            }

            if (basket.LineItems == null || !basket.LineItems.Any())
            {
                return 0;
            }

            decimal sum = 0;
            foreach (var lineItem in basket.LineItems)
            {
                var calculateInputModel = new CalculateInputModel
                {
                    Quantity = lineItem.Amount,
                    TotalPrice = lineItem.Amount * lineItem.ProductListing.Price,
                    UnitPrice = lineItem.ProductListing.Price
                };
                foreach (var discount in lineItem.ProductListing.Discounts)
                {
                    switch (discount.Code)
                    {
                        case "BUY_TO_FREE_QTY":
                            BaseDiscountStrategy strategy = new BuyToFreeQuantityDiscountStrategy(discount.Rules);
                            calculateInputModel = strategy.Calculate(calculateInputModel);
                            break;
                        case "QTY_TO_PCT":
                            strategy = new QuantityToPercentageDiscountStrategy(discount.Rules);
                            calculateInputModel = strategy.Calculate(calculateInputModel);
                            break;
                    }
                }

                sum += calculateInputModel.TotalPrice;
            }

            return sum;
        }

        public async Task<Guid> AddProductListingAsync(Guid basketId, Guid productListingId, uint amount)
        {
            // basket not found.
            var basket = await _basketRepository.GetAsync(basketId);
            if (basket == null)
            {
                throw new ArgumentException("BASKET_NOT_FOUND", nameof(basketId));
            }

            // product listing not found.
            var productListing = await _productListingRepository.GetAsync(productListingId);
            if (productListing == null)
            {
                throw new ArgumentException("PRODUCT_LINE_ITEM_NOT_FOUND", nameof(productListingId));
            }

            var basketLineItemId = await _basketRepository.AddProductLineItemAsync(basketId, productListingId, amount);
            return basketLineItemId;
        }
    }
}