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
{[PermissionChecker(16)]
    public class IndexModel : PageModel
    {
        private IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<ProductGroup> ProductGroups { get; set; }
        public void OnGet()
        {
            ProductGroups = _productService.GetAllGroup();
        }
    }
}