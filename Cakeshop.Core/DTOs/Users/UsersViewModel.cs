using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;

namespace Cakeshop.Core.DTOs.Users
{
    public class UserForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

    }
    public class EditUserViewModel
    {
        public int UserId { get; set; }

        public string OldUserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        public bool IsDelete { get; set; }
        public List<int> UserRoles { get; set; }

    }
}
