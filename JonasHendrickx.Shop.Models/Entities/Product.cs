using System;
using System.Collections.Generic;

namespace JonasHendrickx.Shop.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<ProductListing> ProductListings { get; set; }
    }
}