using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cakeshop.Web.Pages.Admin.Products
{[PermissionChecker(14)]
    public class CreateProductModel : PageModel
    {
        private IProductService _productService;
        private IShopService _shopService;

        public CreateProductModel(IProductService productService, IShopService shopService)
        {
            _productService = productService;
            _shopService = shopService;
        }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            ViewData["Shops"] = _shopService.GetListShops();

            var groups = _productService.GetGroupForManageProduct();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGroups = _productService.GetSubGroupForManageProduct(Convert.ToInt32(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");
        }

        public IActionResult OnPost(IFormFile imgProductUp, List<int> selectedShop)
        {
            if (!ModelState.IsValid)
                return Page();
            int productId = _productService.AddProduct(Product, imgProductUp);
            _shopService.AddShopsToProduct(selectedShop,productId);
            return RedirectToPage("Index");
        }
    }
}