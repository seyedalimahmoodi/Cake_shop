using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cakeshop.Web.Pages.Admin
{[PermissionChecker(19)]
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        private IProductService _productService;
        private IUserService _userService;

        public IndexModel(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }
        [BindProperty]
        public List<Order> Orders { get; set; }
        public void OnGet()
        {
            ViewData["Location"] = _userService.Locations();
            Orders = _orderService.GetOrdersnoHandled();

        }
    }
}