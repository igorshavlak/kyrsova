using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace gergergergerg.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList =  new List<Product>();
        }
        public IdentityUser IdentityUser { get; set; } 
        public IList<Product> ProductList { get; set; }
        //public List<ShoppingCart> ShoppingCartList { get; set; }
    }
}
    