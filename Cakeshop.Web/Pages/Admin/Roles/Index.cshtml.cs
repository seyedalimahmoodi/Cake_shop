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
{[PermissionChecker(9)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> RolesList { get; set; }


        public void OnGet()
        {
            RolesList = _permissionService.GetRoles();
        }

       
    }
}