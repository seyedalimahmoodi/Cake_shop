using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cakeshop.Core.Convertors;
using Cakeshop.Core.DTOs.Products;
using Cakeshop.Core.Generator;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Context;
using Cakeshop.DataLayer.Entities.Product;
using Cakeshop.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cakeshop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly CakeshopContext _context;

        public ProductService(CakeshopContext context)
        {
            _context = context;
        }

        public List<ProductGroup> GetAllGroup()
        {
            return _context.ProductGroups.ToList();
        }
        public List<SelectListItem> GetGroupForManageProduct()
        {
            return _context.ProductGroups.Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageProduct(int groupId)
        {
            return _context.ProductGroups.Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.GroupTitle,
                    Value = g.GroupId.ToString()
                }).ToList();
        }
        public List<ShowProductForAdminViewModel> GetProductsForAdmin()
        {
            return _context.Product.Select(c => new ShowProductForAdminViewModel()
            {
                ProductId = c.ProductId,
                ImageName = c.ProductImageName,
                Title = c.ProductTitle,
                IsExist = c.IsExist,
                Price = c.ProductPrice
            }).ToList();
        }

        public int AddProduct(Product product, IFormFile imgProduct)
        {
            product.ProductImageName = "no-photo.jpg";
            if (imgProduct != null && imgProduct.IsImage())
            {
                product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProduct.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/image", product.ProductImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProduct.CopyTo(stream);
                }
                // ImageConvertor imgResizer = new ImageConvertor();
                // string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb", product.ProductImageName);
                //
                // imgResizer.Image_resize(imagePath, thumbPath, 225);
            }

            _context.Add(product);
            _context.SaveChanges();

            return product.ProductId;
        }
        public Product GetProductById(int productId)
        {
            return _context.Product.Find(productId);
        }

        public void UpdateProduct(Product product, IFormFile imgProduct)
        {

            if (imgProduct != null && imgProduct.IsImage())
            {
                if (product.ProductImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/image", product.ProductImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb", product.ProductImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }
                product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProduct.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/image", product.ProductImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProduct.CopyTo(stream);
                }

                // ImageConvertor imgResizer = new ImageConvertor();
                // string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb", product.ProductImageName);
                //
                // imgResizer.Image_resize(imagePath, thumbPath, 225);
            }

            _context.Product.Update(product);
            _context.SaveChanges();
        }
        public Tuple<List<ShowProductListItemViewModel>, int> GetProduct(int pageId = 1, string filter = "",
                     string orderByType = "نزولی", string getTypeExist = "all", List<int> selectedGroups = null, int take = 0)
        {
            if (take == 0)
                take = 8;

            IQueryable<Product> result = _context.Product;

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.ProductTitle.Contains(filter));
            }

            switch (orderByType)
            {

                case "نزولی":
                    {
                        result = result.OrderByDescending(c => c.ProductPrice);
                        break;
                    }
                case "صعودی":
                    {
                        result = result.OrderBy(c => c.ProductPrice);
                        break;
                    }
            }

            switch (getTypeExist)
            {
                case "all":
                    break;
                case "noExist":
                    result = result.IgnoreQueryFilters().Where(p => p.IsExist == false);
                    break;
                case "isExist":
                    result = result.Where(p => p.IsExist);
                    break;

            }
            //result = result.Include(p => p.ShopProduct).ThenInclude(p => p.ShopId).Select(p => p.);
            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroup == groupId);
                }
            }

            int skip = (pageId - 1) * take;

            int productCount = result.Select(c => new ShowProductListItemViewModel()
            {
                ProductId = c.ProductId,
                ImageName = c.ProductImageName,
                Price = c.ProductPrice,
                Title = c.ProductTitle,
            }).Count();
            int pageCount = productCount / take;
            if ((productCount % take)!=0)
            {
                pageCount++;
            }
            var query = result.Select(c => new ShowProductListItemViewModel()
            {
                ProductId = c.ProductId,
                ImageName = c.ProductImageName,
                Price = c.ProductPrice,
                Title = c.ProductTitle,
            }).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount); ;


        }
        public Product GetProductForShow(int productId)
        {
            return _context.Product
                .FirstOrDefault(c => c.ProductId == productId);
        }
        public void AddComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
            _context.SaveChanges();
        }

        public List<ProductComment> GetProductComment(int productId, int pageId = 1)
        {


            return _context.ProductComments.Include(c => c.User).Where(c => !c.IsDelete && c.ProductId == productId)
                    .OrderByDescending(c => c.CreateDate).ToList();
        }
        public List<ShowProductListItemViewModel> GetPopularProduct()
        {
            return _context.Product.Include(c => c.OrderDetails)
                .Where(c => c.OrderDetails.Any())
                .OrderByDescending(d => d.OrderDetails.Count)
                .Take(8)
                .Select(c => new ShowProductListItemViewModel()
                {
                    ProductId = c.ProductId,
                    ImageName = c.ProductImageName,
                    Price = c.ProductPrice,
                    Title = c.ProductTitle,
                })
                .ToList();
        }
        public ProductGroup GetById(int groupId)
        {
            return _context.ProductGroups.Find(groupId);
        }

        public void AddGroup(ProductGroup @group)
        {
            _context.ProductGroups.Add(group);
            _context.SaveChanges();
        }

        public void UpdateGroup(ProductGroup @group)
        {
            _context.ProductGroups.Update(group);
            _context.SaveChanges();
        }

        public List<Product> GetListProductsByShopId(int id=5)
        {
            var a =_context.Product.Include(c => c.ShopProduct)
                .Where(c => c.ShopProduct.Exists(sp => sp.ShopId == id)).ToList();
            return a;
        }


    }
}
