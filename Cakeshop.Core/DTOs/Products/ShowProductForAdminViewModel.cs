using System;
using System.Collections.Generic;
using System.Text;

namespace Cakeshop.Core.DTOs.Products
{
   public class ShowProductForAdminViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public bool IsExist { get; set; }
    }
}
