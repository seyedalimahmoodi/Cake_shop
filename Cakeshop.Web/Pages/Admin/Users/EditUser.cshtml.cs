using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cakeshop.Core.DTOs;
using Cakeshop.Core.DTOs.Users;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;

namespace Cakeshop.Web.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }




        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }
        public void OnGet(int id)
        {
            EditUserViewModel = _userService.GetUserForShowInEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                EditUserViewModel = _userService.GetUserForShowInEditMode(EditUserViewModel.UserId);
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }

            if (_userService.IsExistEmailForEdit(FixedText.FixEmail(EditUserViewModel.Email), EditUserViewModel.OldUserName))
            {
                EditUserViewModel = _userService.GetUserForShowInEditMode(EditUserViewModel.UserId);
                ViewData["Roles"] = _permissionService.GetRoles();
                ViewData["IsExistEmail"] = true;
                return Page();
            }
            if (_userService.IsExistUserNameForEdit(EditUserViewModel.UserName, EditUserViewModel.OldUserName))
            {
                EditUserViewModel = _userService.GetUserForShowInEditMode(EditUserViewModel.UserId);
                ViewData["Roles"] = _permissionService.GetRoles();
                ViewData["IsExistUserName"] = true;
                return Page();
            }



            _userService.EditUserFromAdmin(EditUserViewModel);

            //Edit Roles
            _permissionService.EditRolesUser(EditUserViewModel.UserId, selectedRoles);
            return RedirectToPage("Index");
        }
    }
}