using Core.Entities;
using Data.Contexts;
using Data.Repistories.Abstract;
using Data.Repistories.Base;

namespace Data.Repistories.Concrete;

public class AdminRepistory : Repistory<Admin>, IAdminRepistory
{
    private readonly AppDbContext _context;
    public AdminRepistory(AppDbContext context) : base(context)
    {
        _context = context;
    }

    
    public Admin GetAdminByEmail(string email)
    {
        return _context.Admins.FirstOrDefault(x=>x.Email==email);
    }

}
