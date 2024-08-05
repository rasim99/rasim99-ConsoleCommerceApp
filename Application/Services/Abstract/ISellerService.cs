
using Core.Entities;

namespace Application.Services.Abstract;

public interface ISellerService
{
    Seller Login();

    void ShowSelledProducts();
    void ShowSelledProductsByDate();
    void ShowFilteredProducts();
    void ShowTotatlIncome();

    void CreateProduct();
    void ChangeProductCount();
    void DeleteProduct();
}
