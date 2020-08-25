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
{
    [PermissionChecker(17)]
    public class CreateGroupModel : PageModel
    {
        private IProductService _productService;

        public CreateGroupModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductGroup ProductGroups { get; set; }

        public void OnGet(int? id)
        {
            ProductGroups=new ProductGroup()
            {
                ParentId = id
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _productService.AddGroup(ProductGroups);

            return RedirectToPage("Index");
        }
    }
}