using System;
using System.Collections.Generic;
using System.Text;
using Cakeshop.Core.DTOs.Shops;
using Cakeshop.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Http;

namespace Cakeshop.Core.Services.Interfaces
{
   public interface IShopService
    {
        #region Shops

        List<ShopViewModel> GetShops();
        List<Shop> GetActiveShops(int productId);
        List<Shop> GetInactiveShops(int productId);
        List<Shop> GetListShops();
        int AddShop(Shop shop, IFormFile imgShop);
        Shop GetShopById(int shopId);

        void UpdateShop(Shop shop, IFormFile imgShop);
        void DeleteShop(int shopId);
        void AddShopsToProduct(List<int> shopIds, int productId);
        void EditShopsProduct(int productId, List<int> shopsId);

        #endregion
    }
}
