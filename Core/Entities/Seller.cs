
using Core.Entities.Base;

namespace Core.Entities;

public class Seller : PersonalInfo
{
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
}
