using System;
using System.ComponentModel.DataAnnotations;

namespace Cakeshop.DataLayer.Entities.Product
{
    public class ProductComment
    {
        [Key]
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [MaxLength(700)]
        [Required(ErrorMessage = "لطفا متنی را برای ارسال تایپ کنید.")]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }


        public Product Product { get; set; }
        public User.User User { get; set; }
    }
}
