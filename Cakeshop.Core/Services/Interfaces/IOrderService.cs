using System.Collections.Generic;
using Cakeshop.Core.DTOs.Orders;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.Product;

namespace Cakeshop.Core.Services.Interfaces
{
   public interface IOrderService
   {
       int AddOrder(string userName, int productId);
       void UpdateCountOrderDetail(int id, string command);
       void UpdatePriceOrder(int orderId);

       Order GetOrderForUserPanel(string userName);
       Order GetOrderByUserName(string userName);
       List<Order> GetOrdersnoHandled();
       void DeleteOrderDetail(int id);

       List<ShowOrderViewModel> GetListActiveOrderDetails(string userName);
        bool FinalyOrder(string userName);
   }
}
