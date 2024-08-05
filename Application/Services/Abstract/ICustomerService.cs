
using Core.Entities;

namespace Application.Services.Abstract;

public interface ICustomerService
{
    Customer Login();
    void ShowBuyedProducts();
    void ShowOrderByDate();
    void ShowFilteredProduct();
    void CreateOrder();
}
