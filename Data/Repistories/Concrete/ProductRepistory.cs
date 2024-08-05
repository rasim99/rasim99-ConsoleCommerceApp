
using Core.Entities;
using Data.Contexts;
using Data.Repistories.Abstract;
using Data.Repistories.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repistories.Concrete;

public class ProductRepistory : Repistory<Product>, IProductRepistory
{
    private readonly AppDbContext _context;
    public ProductRepistory(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public Product GetExistProduct(string name,int id)
    {
        return _context.Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower()&&p.SellerId==id);
    }

   public List<Product> GetAllBySeller(int id)
    {
        return _context.Products.Where(p=>p.SellerId==id).ToList();
    }
    
    public List<Product> GetAllByName(string name)
    {
        return _context.Products.Where(p=>p.Name.ToLower()==name.ToLower()).ToList();
    }

    public List<Product> FilteredProducts(int sellerId, string nameCharacter)
    {
        return _context.Products
    .Include(x => x.Seller)
    .Where(x => x.SellerId == sellerId && x.Name.Contains(nameCharacter)).ToList();
    }
}
