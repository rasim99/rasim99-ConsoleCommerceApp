
using Core.Entities;
using Data.Repistories.Base;

namespace Data.Repistories.Abstract;

public interface IOrderRepoistory :IRepistory<Order>
{
    List<Order> GetAllOrdersOrderByDesc();
     List<Order> GetAllOrderByCustomer(int id);
     List<Order> GetAllOrderBySeller(int id);
     List<Order> GetAllOrdersByCustomerId(int id);
     List<Order> GetAllSelledProductByDate(int sellerId, DateTime date);
    List<Order> GetAllOrderByCustomerWithDate(int customerId, DateTime date);
    List<Order> GetOrdersByProductSymbol(int customerId, string namesymbol);
     List<Order> GetAllOrdersByDate(DateTime date);
}
