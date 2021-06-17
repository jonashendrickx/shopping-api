namespace JonasHendrickx.Shop.Contracts.Discounts
{
    public class CalculateInputModel
    {
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}