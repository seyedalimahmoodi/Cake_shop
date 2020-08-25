using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Product;

namespace Cakeshop.Web.Pages.Admin.ProductGroups
{[PermissionChecker(18)]
    public class EditGroupModel : PageModel
    {
        private IProductService _courseService;

        public EditGroupModel(IProductService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public ProductGroup ProductGroups { get; set; }

        public void OnGet(int id)
        {
            ProductGroups = _courseService.GetById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateGroup(ProductGroups);

            return RedirectToPage("Index");
        }
    }
}