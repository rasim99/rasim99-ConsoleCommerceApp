using Core.Constants;
using Data.Contexts;
using Data.Repistories.Concrete;
using Data.UnitOfWorks.Abstract;
namespace Data.UnitOfWorks.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public readonly AdminRepistory Admins;
    public readonly SellerRepistory Sellers;
    public readonly CustomerRepistory Customers;
    public readonly CategoryRepistory Categories;
    public readonly ProductRepistory Products;
    public readonly OrderRepistory Orders;
   
    public UnitOfWork()
    {
        _context = new ();
        Admins = new(_context);
        Sellers = new(_context);
        Customers = new(_context);
        Categories = new(_context);
        Products = new(_context);
        Orders = new(_context);
       
    }
    public void Commit()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (Exception)
        {

            Messages.ErrorOccuredMessage();
        }
    }
}
