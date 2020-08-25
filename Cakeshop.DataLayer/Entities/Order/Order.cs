using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cakeshop.DataLayer.Entities.User;

namespace Cakeshop.DataLayer.Entities.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OrderSum { get; set; }

        public bool IsFinaly { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; } = false;

        [ForeignKey("UserLocationId")]
        public virtual UserLocation UserLocation { get; set; }
        public virtual User.User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
