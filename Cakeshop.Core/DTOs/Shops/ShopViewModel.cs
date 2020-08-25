using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cakeshop.Core.DTOs.Shops
{
   public class ShopViewModel
    {
        public int ShopId { get; set; }
        public string ShopTitle { get; set; }

        public string ShopAddress { get; set; }

        public string Phone { get; set; }
    }
}
