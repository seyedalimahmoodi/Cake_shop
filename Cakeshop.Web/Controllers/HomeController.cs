using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cakeshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cakeshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IProductService _productService;
        private IShopService _shopService;

        public HomeController(IUserService userService, IProductService productService, IShopService shopService)
        {
            _userService = userService;
            _productService = productService;
            _shopService = shopService;
        }

        public IActionResult Index()
        {
            ViewData["Shops"] = _shopService.GetListShops();
            var popular = _productService.GetPopularProduct();
            ViewBag.PopularProduct = popular;
            return View(_productService.GetProduct(getType: "isExist",take:8).Item1);
        }
        [Route("OnlinePayment/{id}")]
        public IActionResult onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var cart = _userService.GetCartByCartId(id);

                var payment = new ZarinpalSandbox.Payment(cart.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    cart.IsPay = true;
                    _userService.UpdateCart(cart);
                }

            }

            return View();
        }
        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange(_productService.GetSubGroupForManageProduct(id));
            return Json(new SelectList(list, "Value", "Text"));
        }
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"/MyImages/{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}