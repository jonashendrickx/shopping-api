using System.Collections.Generic;
using JonasHendrickx.Shop.Contracts.Discounts;
using Newtonsoft.Json;

namespace JonasHendrickx.Shop.Services.Discounts
{
    public abstract class BaseDiscountStrategy
    {
        protected readonly Dictionary<string, string> Input;
        
        protected BaseDiscountStrategy(string input)
        {
            Input = Parse(input);
        }
        
        public string Code { get; protected set; }

        public abstract decimal Calculate(CalculateInputModel criteria);

        private Dictionary<string, string> Parse(string input)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(input);
            return dictionary;
        }
    }
}