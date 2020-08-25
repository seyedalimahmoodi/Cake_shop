 using System;
using System.Collections.Generic;
using System.Linq;
using Cakeshop.Core.DTOs.Orders;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Context;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.Product;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Cakeshop.Core.Services
{
    public class OrderService : IOrderService
    {
        private CakeshopContext _context;
        private IUserService _userService;

        public OrderService(CakeshopContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public int AddOrder(string userName, int productId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            Order order = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

            var product = _context.Product.Find(productId);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    OrderSum = product.ProductPrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            ProductId = productId,
                            Count = 1,
                            Price = product.ProductPrice
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail detail = _context.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productId);
                if (detail != null)
                {
                    detail.Count += 1;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        ProductId = productId,
                        Price = product.ProductPrice,
                    };
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }


            return order.OrderId;

        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price * d.Count);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Order GetOrderForUserPanel(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            int orderId = _context.Orders.Find(userId).OrderId;
            return _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public List<ShowOrderViewModel> GetListActiveOrderDetails(string userName)
        {
            int userid = _userService.GetUserIdByUserName(userName);



            Order order = _context.Orders.SingleOrDefault(o => o.UserId == userid && !o.IsFinaly);

            List<ShowOrderViewModel> list = new List<ShowOrderViewModel>();
            if (order != null)
            {
                var details = _context.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                foreach (var item in details)
                {
                    var product = _context.Product.Find(item.ProductId);

                    list.Add(new ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ImageName = product.ProductImageName,
                        OrderDetailId = item.DetailId,
                        Price = item.Price,
                        Sum = item.Count * item.Price,
                        Title = product.ProductTitle,
                        OrderId = item.OrderId
                    });

                }
            }

            return list;

        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            _context.Remove(orderDetail);
            _context.SaveChanges();
        }

        public void UpdateCountOrderDetail(int id, string command)
        {
            var orderDetail = _context.OrderDetails.Find(id);

            switch (command)
            {
                case "up":
                    {
                        orderDetail.Count += 1;
                        _context.Update(orderDetail);
                        break;
                    }
                case "down":
                    {
                        orderDetail.Count -= 1;
                        if (orderDetail.Count == 0)
                        {
                            _context.OrderDetails.Remove(orderDetail);
                        }
                        else
                        {
                            _context.Update(orderDetail);
                        }

                        break;
                    }
            }


            _context.SaveChanges();
        }

        public Order GetOrderByUserName(string userName)
        {
            int userid = _userService.GetUserIdByUserName(userName);



            Order order = _context.Orders.SingleOrDefault(o => o.UserId == userid && !o.IsFinaly);
            return order;
        }

        public bool FinalyOrder(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .FirstOrDefault(o =>   o.UserId == userId);

            if (order == null || order.IsFinaly)
            {
                return false;
            }


            order.IsFinaly = true;

            _context.Orders.Update(order);

            _context.SaveChanges();
            return true;

        }

        public List<Order> GetOrdersnoHandled()
        {
            return _context.Orders.Include(o=>o.UserLocation).Include(o=>o.OrderDetails).ThenInclude(od=>od.Product).Where(o => o.IsFinaly).ToList();
        }
    }
}
