using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cakeshop.DataLayer.Entities.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }

        [ForeignKey("OrderId")]
        public virtual Cakeshop.DataLayer.Entities.Order.Order Order { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product.Product Product { get; set; }

    }
}
