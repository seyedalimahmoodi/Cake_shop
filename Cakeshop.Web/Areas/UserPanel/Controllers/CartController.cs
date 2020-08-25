using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cakeshop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class CartController : Controller
    {
        private IUserService _userService;
        private IOrderService _orderService;

        public CartController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [Route("UserPanel/PurchaseInvoice")]
        public IActionResult PurchaseInvoice()
        {
            return View(_userService.GetCartUser(User.Identity.Name));
        }

        [Route("UserPanel/Cart")]
        public ActionResult Index()
        {
            if (!ModelState.IsValid)
            {

                return View(_userService.GetCartUser(User.Identity.Name));
            }

            int cartId = _userService.ChargeCart(User.Identity.Name, _userService.GetAmountCarts(User.Identity.Name), "شارژ حساب");


            #region Online Payment

            var payment = new ZarinpalSandbox.Payment(_userService.GetAmountCarts(User.Identity.Name));

            var res = payment.PaymentRequest("شارژ کیف پول", "https://Foroghefarda.ir/OnlinePayment/" + cartId);

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion
            return null;
        }


    }
}