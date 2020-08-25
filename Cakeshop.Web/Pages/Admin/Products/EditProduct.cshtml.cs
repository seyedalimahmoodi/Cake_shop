using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Product;

namespace Cakeshop.Web.Pages.Admin.Products
{[PermissionChecker(15)]
    public class EditProductModel : PageModel
    {
        private IProductService _productService;
        private IShopService _shopService;

        public EditProductModel(IProductService productService, IShopService shopService)
        {
            _productService = productService;
            _shopService = shopService;
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productService.GetProductById(id);
            ViewData["ActiveShops"] = _shopService.GetActiveShops(id);

            ViewData["InactiveShops"] = _shopService.GetInactiveShops(id);

            var groups = _productService.GetGroupForManageProduct();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Product.GroupId);

            var subGroups = _productService.GetSubGroupForManageProduct(Convert.ToInt32(Product.GroupId));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Product.SubGroup ?? 0);
        }

        public IActionResult OnPost(IFormFile imgProductUp, List<int> SelectedShop)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActiveShops"] = _shopService.GetActiveShops(Product.ProductId);

                ViewData["InactiveShops"] = _shopService.GetInactiveShops(Product.ProductId);

                var groups = _productService.GetGroupForManageProduct();
                ViewData["Groups"] = new SelectList(groups, "Value", "Text", Product.GroupId);

                var subGroups = _productService.GetSubGroupForManageProduct(Convert.ToInt32(Product.GroupId));
                ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Product.SubGroup ?? 0);
                return Page();
            }

            _shopService.EditShopsProduct(Product.ProductId, SelectedShop);
            _productService.UpdateProduct(Product, imgProductUp);
            return RedirectToPage("Index");
        }
    }
}