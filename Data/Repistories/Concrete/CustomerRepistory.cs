using Core.Entities;
using Data.Contexts;
using Data.Repistories.Abstract;
using Data.Repistories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repistories.Concrete
{
    public class CustomerRepistory : Repistory<Customer>, ICustomerRepistory
    {
        private readonly AppDbContext _context;

        public CustomerRepistory(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Customer GetExistCustomer(string item)
        {
            return _context.Customers
                .FirstOrDefault(x => x.Email == item || x.SerialNumber.ToLower() == item || x.Pin.ToLower() == item ||
                x.PhoneNumber==item);

        }
        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(y => y.Email == email);
        }
    }
}
