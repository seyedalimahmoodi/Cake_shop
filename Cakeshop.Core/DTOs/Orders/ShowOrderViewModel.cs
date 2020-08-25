namespace Cakeshop.Core.DTOs.Orders
{
    public class ShowOrderViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }

    }
}
