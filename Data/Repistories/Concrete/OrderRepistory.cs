
using Core.Entities;
using Data.Contexts;
using Data.Repistories.Abstract;
using Data.Repistories.Base;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Repistories.Concrete;

public class OrderRepistory : Repistory<Order>, IOrderRepoistory
{
    private readonly AppDbContext _context;
    public OrderRepistory(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Order> GetAllOrdersOrderByDesc()
    {
            return _context.Orders
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.Seller)
        .OrderByDescending(x=>x.CreateAt).ToList();
    }   
    
    public List<Order> GetAllOrderByCustomer(int id)
    {
            return _context.Orders
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.Seller)
        .Where(x => x.CustomerId==id).ToList();
    } 

    public List<Order> GetAllOrderBySeller(int id)
    {
            return _context.Orders
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.Seller)
        .Where(x => x.SellerId==id).ToList();
    }

    public List<Order> GetAllOrdersByCustomerId(int id)
    {
        return _context.Orders
    .Include(x => x.Customer)
    .Include(x => x.Product)
    .Include(x => x.Seller)
    .Where(x => x.CustomerId == id).ToList();
    }

    public List<Order> GetAllSelledProductByDate(int sellerId, DateTime date)
    {
            return _context.Orders
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.Seller)
        .Where(x =>x.SellerId==sellerId&&x.CreateAt.Date==date.Date).ToList();
    } 
    
    public List<Order> GetAllOrderByCustomerWithDate(int customerId, DateTime date)
    {
            return _context.Orders
        .Include(x => x.Customer)
        .Include(x => x.Product)
        .Include(x => x.Seller)
        .Where(x =>x.CustomerId==customerId&&x.CreateAt.Date==date.Date).ToList();
    }


    public List<Order> GetOrdersByProductSymbol( int customerId , string namesymbol)
    {
        return _context.Orders
            .Include(s => s.Seller)
            .Include(s => s.Customer)
            .Include(s => s.Product)
            .Where(x => x.Product.Name.Contains(namesymbol) && x.CustomerId == customerId).ToList();
    }


    public List<Order> GetAllOrdersByDate(DateTime date)
    {
        return _context.Orders
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.Seller)
            .Where(x => x.CreateAt.Date==date.Date).ToList();
    }
}
