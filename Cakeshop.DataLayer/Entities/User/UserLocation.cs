using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cakeshop.DataLayer.Entities.User
{
    public class UserLocation
    {
        [Key]
        public int UserLocationId { get; set; }

        [Display(Name = "آدرس کامل")]
        [Required(ErrorMessage = "لطفا آدرس محل تحویل را به وارد کنید.")]
        [MaxLength(1500, ErrorMessage = "آدرس نمی تواند بیشتر از 1500 کاراکتر باشد.")]
        public string Address { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا کد پستی خود را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "کد پستی نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string PostalCode { get; set; }

        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا شماره تماستان را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "شماره تماس نمی تواند بیشتر از 50 کاراکتر باشد.")]
        public string Phone { get; set; }

        public virtual Order.Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
