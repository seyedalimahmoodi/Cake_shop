using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cakeshop.DataLayer.Entities.Product
{
    public class ProductGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }
        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")] 
        public int? ParentId { get; set; }

        [Display(Name = "تصویر گروه")]
        public string ImageName { get; set; }



        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }

        [InverseProperty("ProductGroup")]
        public List<Product> Products { get; set; }

        [InverseProperty("Group")]
        public List<Product> SubGroup { get; set; }

    }
}
