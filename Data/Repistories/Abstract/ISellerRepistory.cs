using Core.Entities;
using Data.Repistories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repistories.Abstract
{
    public interface ISellerRepistory : IRepistory<Seller>
    {
        Seller GetExistSeller(string item);
        Seller GetSellerByEmail(string email);

    }
}
