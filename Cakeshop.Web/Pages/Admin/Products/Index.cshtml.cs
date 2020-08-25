using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.DTOs.Products;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cakeshop.Web.Pages.Admin.Products
{[PermissionChecker(13)]
    public class IndexModel : PageModel
    {
        private IProductService _courseService;

        public IndexModel(IProductService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowProductForAdminViewModel> ListProduct { get; set; }

        public void OnGet()
        {
            ListProduct = _courseService.GetProductsForAdmin();
        }
    }
}