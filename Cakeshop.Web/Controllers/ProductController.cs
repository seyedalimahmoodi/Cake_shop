using System;
using System.Collections.Generic;
using System.Linq;
using Cakeshop.Core.DTOs.Products;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cakeshop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private IUserService _userService;
        private IShopService _shopService;

        public ProductController(IProductService productService, IOrderService orderService, IUserService userService, IShopService shopService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
            _shopService = shopService;
        }

        public IActionResult Index(int pageId = 1, string filter = "",string orderByType = "نزولی", string getTypeExist = "all", List<int> selectedGroups = null)
        {
            ViewData["ProductGroups"] = _productService.GetAllGroup().Take(6);
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.Groups = _productService.GetAllGroup();
            ViewBag.pageId = pageId;
            return View(_productService.GetProduct(pageId, filter, orderByType, getTypeExist, selectedGroups, 12));
        }
        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProductForShow(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize]
        public ActionResult BuyProduct(int id)
        {
          int orderId =  _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/ShowProduct/" + id);
        }
        [HttpPost]
        public IActionResult CreateComment(ProductComment comment)
        {
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _productService.AddComment(comment);

            return View("ShowComment", _productService.GetProductComment(comment.ProductId));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_productService.GetProductComment(id, pageId));
        }

    }
}