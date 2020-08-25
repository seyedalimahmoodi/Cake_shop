using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.Shop;

namespace Cakeshop.DataLayer.Entities.Product
{
    public class Product
    {
        public Product()
        {
            
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }


        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProductTitle { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductDescription { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductPrice { get; set; }

        [MaxLength(50)]
        public string ProductImageName { get; set; }

        [Display(Name = "موجود است؟")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsExist { get; set; } = true;

        [Display(Name = "تعداد موجود")]
        public int? Count { get; set; }

        public bool IsDelete { get; set; } = false;

        #region Relations

        [ForeignKey("GroupId")]
        public ProductGroup ProductGroup { get; set; }

        [ForeignKey("SubGroup")]
        public ProductGroup Group { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ProductComment> ProductComments { get; set; }

        public List<ShopProduct> ShopProduct { get; set; }

        #endregion

    }
}
