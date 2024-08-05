
using Core.Entities;
using Data.Repistories.Base;

namespace Data.Repistories.Abstract;

public interface ICustomerRepistory : IRepistory<Customer>
{
    Customer GetExistCustomer(string item);
    Customer GetCustomerByEmail(string email);

}
