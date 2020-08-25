using Cakeshop.Core.DTOs;
using Cakeshop.Core.DTOs.Users;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cakeshop.Web.Pages.Admin.Users
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel UserForAdminViewModel { get; set; }

        public void OnGet(int pageId=1,string filterUserName="",string filterEmail="")
        {
            UserForAdminViewModel = _userService.GetUsers(pageId,filterEmail,filterUserName);
        }

       
    }
}