﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Cakeshop.Web.Pages.Admin.Roles
{[PermissionChecker(11)]
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}