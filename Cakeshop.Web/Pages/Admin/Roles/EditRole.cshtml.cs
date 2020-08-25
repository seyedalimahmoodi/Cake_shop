using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Cakeshop.Web.Pages.Admin.Roles
{[PermissionChecker(12)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                Role = _permissionService.GetRoleById(Role.RoleId);
                ViewData["Permissions"] = _permissionService.GetAllPermission();
                ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(Role.RoleId);
                return Page();
            }

            _permissionService.UpdateRole(Role);

            _permissionService.UpdatePermissionsRole(Role.RoleId, SelectedPermission);


            return RedirectToPage("Index");
        }
    }
}