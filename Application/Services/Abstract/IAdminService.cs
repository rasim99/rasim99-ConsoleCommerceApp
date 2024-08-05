
using Core.Entities;

namespace Application.Services.Abstract;

public interface IAdminService
{
    Admin Login();
    void ShowAllCustomer();
    void ShowAllSeller();
    void ShowOrderByDate();
    void ShowOrdersOrderBy();
    void ShowOrderByCustomer();
    void ShowOrderBySeller();


    void CreateSeller();
    void CreateCustomer();
    void CreateCategory();
    void DeleteSeller();
    void DeleteCustomer();

}
