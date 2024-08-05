using Core.Entities;
using Data.Contexts;
using Data.Repistories.Abstract;
using Data.Repistories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repistories.Concrete
{
    public class SellerRepistory : Repistory<Seller>, ISellerRepistory
    {
        private readonly AppDbContext _context;

        public SellerRepistory(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Seller GetExistSeller(string item)
        {
            return _context.Sellers
                .FirstOrDefault(x=>x.Email==item||x.SerialNumber.ToLower()==item ||x.Pin.ToLower()==item
                || x.PhoneNumber==item);
        }

        public Seller GetSellerByEmail(string email)
        {
            return _context.Sellers.FirstOrDefault(y=>y.Email==email);
        }
    }
}
