using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cakeshop.DataLayer.Entities.Shop
{
   public class ShopProduct
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public int ProductId { get; set; }
        public Product.Product Product { get; set; }
    }
}
