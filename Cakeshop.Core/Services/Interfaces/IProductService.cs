using System;
using System.Collections.Generic;
using System.Text;
using Cakeshop.Core.DTOs.Products;
using Cakeshop.DataLayer.Entities.Product;
using Cakeshop.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cakeshop.Core.Services.Interfaces
{
   public interface IProductService
    {
        #region Group

        List<ProductGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageProduct();
        List<SelectListItem> GetSubGroupForManageProduct(int groupId);
        ProductGroup GetById(int groupId);
        void AddGroup(ProductGroup group);
        void UpdateGroup(ProductGroup group);

        #endregion

        #region Product

        List<ShowProductForAdminViewModel> GetProductsForAdmin();

        int AddProduct(Product product, IFormFile imgProduct);
        Product GetProductById(int productId);
        void UpdateProduct(Product product, IFormFile imgproduct);
        Tuple<List<ShowProductListItemViewModel>, int> GetProduct(int pageId = 1, string filter = "",
            string orderByType = "price", string getType = "all", List<int> selectedGroups = null, int take = 0);
        Product GetProductForShow(int productId);
        List<ShowProductListItemViewModel> GetPopularProduct();
        List<Product> GetListProductsByShopId(int id=5);
        
        #endregion
        #region Comments

        void AddComment(ProductComment comment);
        List<ProductComment> GetProductComment(int ProductId, int pageId = 1);

        #endregion
    }
}
