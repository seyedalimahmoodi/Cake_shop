using System.Collections.Generic;
using Cakeshop.Core.DTOs.Orders;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cakeshop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public MyOrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrderDetail(id);
            return RedirectToAction("ShowOrderDetails");
        }

        public IActionResult UpdateCount(int id, string command)
        {
            _orderService.UpdateCountOrderDetail(id, command);
            return RedirectToAction("ShowOrderDetails");
        }

        public IActionResult AddToCart(int productId)
        {
            _orderService.AddOrder(User.Identity.Name, productId);

            return Redirect("/ShowProduct/" + productId);
        }
        public IActionResult ShowOrderDetails(bool finaly = false)
        {
            List<ShowOrderViewModel> orderDetails = _orderService.GetListActiveOrderDetails(User.Identity.Name);

            if (orderDetails == null)
            {
                return NotFound();
            }

            ViewBag.finaly = finaly;
            return View(orderDetails);
        }

        public IActionResult GetUserLocation(int orderId)
        {
            ViewData["OrderSum"] = _orderService.GetOrderByUserName(User.Identity.Name).OrderSum;
            
            return View();
        }
        [HttpPost]
        public IActionResult GetUserLocation(UserLocation location)
        {
            if (!ModelState.IsValid)
            {
                ViewData["OrderSum"] = _orderService.GetOrderByUserName(User.Identity.Name).OrderSum;
                return View();
            }
            _userService.AddUserLocation(location);
            _orderService.FinalyOrder(User.Identity.Name);
            return Redirect("/");

        }

    }
}