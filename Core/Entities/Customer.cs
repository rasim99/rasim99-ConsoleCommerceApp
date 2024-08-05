
using Core.Entities.Base;

namespace Core.Entities;

public class Customer : PersonalInfo
{
   public ICollection<Order> Orders {  get; set; } 

}
