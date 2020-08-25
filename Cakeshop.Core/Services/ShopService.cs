using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cakeshop.Core.DTOs.Shops;
using Cakeshop.Core.Generator;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Context;
using Cakeshop.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cakeshop.Core.Services
{
    public class ShopService : IShopService
    {
        private CakeshopContext _context;

        public ShopService(CakeshopContext context)
        {
            _context = context;
        }

        public List<ShopViewModel> GetShops()
        {
            return _context.Shops.Select(s => new ShopViewModel()
            {
                ShopId = s.ShopId,
                ShopTitle = s.ShopTitle,
                ShopAddress = s.ShopAddress,
                Phone = s.Phone
            }).ToList();
        }
        public List<Shop> GetListShops()
        {
            return _context.Shops.ToList();
        }
        public int AddShop(Shop shop, IFormFile imgShop)
        {
            shop.ImageName = "no-photo.jpg";

            if (imgShop != null && imgShop.IsImage())
            {
                shop.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgShop.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/shop", shop.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgShop.CopyTo(stream);
                }

            }
            _context.Shops.Add(shop);
            _context.SaveChanges();
            return shop.ShopId;
        }
        public Shop GetShopById(int shopId)
        {
            return _context.Shops.Find(shopId);
        }

        public void UpdateShop(Shop shop, IFormFile imgShop)
        {
            if (imgShop != null && imgShop.IsImage())
            {
                if (shop.ImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/shop", shop.ImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/shop", shop.ImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                shop.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgShop.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/shop", shop.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgShop.CopyTo(stream);
                }


            }
            _context.Shops.Update(shop);
            _context.SaveChanges();
        }
        public void DeleteShop(int shopId)
        {
            Shop shop = GetShopById(shopId);
            shop.IsDelete = true;
            _context.Shops.Update(shop);
            _context.SaveChanges();
        }
        public void AddShopsToProduct(List<int> shopIds, int productId)
        {
            foreach (int shopId in shopIds)
            {
                _context.ShopProducts.Add(new ShopProduct()
                {
                    ShopId = shopId,
                    ProductId = productId
                });
            }

            _context.SaveChanges();
        }

        public void EditShopsProduct(int productId, List<int> shopsId)
        {
            //Delete All Shops Product
            _context.ShopProducts.Where(r => r.ProductId == productId).ToList().ForEach(r => _context.ShopProducts.Remove(r));

            //Add New Shops
            AddShopsToProduct(shopsId, productId);
        }

        public List<Shop> GetActiveShops(int productId)
        {
            return _context.Shops.Include(s => s.ShopProduct)
                .Where(s => s.ShopProduct.Exists(sp => sp.ProductId == productId)).ToList();


        }

        public List<Shop> GetInactiveShops(int productId)
        {
            return _context.Shops.Include(s => s.ShopProduct)
                .Where(sp => sp.ShopProduct.Any(p=> p.ProductId==productId)==false).ToList();
        }
    }
}
