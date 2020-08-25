using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cakeshop.DataLayer.Entities.Shop
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }

        [Display(Name = "نام شعبه")]
        [MaxLength(100, ErrorMessage = "نام شعبه نمی تواند بیشتر از 100 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا نام شعبه را وارد کنید")]
        public string ShopTitle { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "آدرس شعبه")]
        [MaxLength(1000, ErrorMessage = "آدرس شعبه نمی تواند بیشتر از 1000 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا آدرس شعبه را وارد کنید")]
        public string ShopAddress { get; set; }

        [Display(Name = "شماره تماس")]
        [MaxLength(1000, ErrorMessage = "شماره تماس نمی تواند بیشتر از 1000 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا شماره تماس را وارد کنید")]
        public string Phone { get; set; }

        [Display(Name = "تصویر شعبه")]
        [MaxLength(100)]
        public string ImageName { get; set; }

        public bool IsDelete { get; set; }

        public List<ShopProduct> ShopProduct { get; set; }

    }
}
