using System;

namespace Cakeshop.Core.DTOs.Products
{
    public class ShowProductListItemViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
    }
}
