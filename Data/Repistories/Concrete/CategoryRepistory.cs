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
    public class CategoryRepistory : Repistory<Category>,ICategoryRepistory
    {
        private readonly AppDbContext _context;
        public CategoryRepistory(AppDbContext context) : base(context)
        {

            _context = context;
        }

        public Category GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
