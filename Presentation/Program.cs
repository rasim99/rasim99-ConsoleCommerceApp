using Application.Services.Abstract;
using Application.Services.Concrete;
using Core.Constants;
using Core.Extensions;
using Data;
using Data.UnitOfWorks.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Presentation;

internal static class Program
{

    private static UnitOfWork _unitOfWork;
    private static AdminService _adminService;
    private static CustomerService _customerService;
    private static SellerService _sellerService;
    static Program()
    {
        _unitOfWork = new UnitOfWork();
        _adminService = new(_unitOfWork);
        _customerService = new(_unitOfWork);
        _sellerService = new(_unitOfWork);
    }
    static void Main(string[] args)
    {
    
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DbInitializer.SeedData();
        while (true)
        {
        SelectInput: Console.WriteLine("✍🏿 chooese one : admin , seller , customer or end ");
            string select = Console.ReadLine();
            if (select.ToLower() == "admin")
            {
            ContinueAdminInput: var admin = _adminService.Login();
                if (admin is not null)
                {
                    Messages.WelcomeMessage($" 😎😎 {admin.Name}  {admin.Surname}");
                    while (true)
                    {
                        ShowAdminMenu();
                    AdminChooseInput: Messages.InputMessage("choice");
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((AdminOperations)choice)
                            {
                                case AdminOperations.MainMenu:
                                    goto SelectInput;
                                case AdminOperations.ShowAllCustomer:
                                    _adminService.ShowAllCustomer();
                                    break;
                                case AdminOperations.ShowAllSeller:
                                    _adminService.ShowAllSeller();
                                    break;
                                case AdminOperations.ShowOrdersOrderBy:
                                    _adminService.ShowOrdersOrderBy();
                                    break;
                                case AdminOperations.ShowOrderByCustomer:
                                    _adminService.ShowOrderByCustomer();
                                    break;
                                case AdminOperations.ShowOrderBySeller:
                                    _adminService.ShowOrderBySeller();
                                    break;
                                case AdminOperations.ShowOrderByDate:
                                    _adminService.ShowOrderByDate();
                                    break;
                                case AdminOperations.CreateSeller:
                                    _adminService.CreateSeller();
                                    break;
                                case AdminOperations.CreateCustomer:
                                    _adminService.CreateCustomer();
                                    break;
                                case AdminOperations.CreateCategory:
                                    _adminService.CreateCategory();
                                    break;

                                case AdminOperations.DeleteSeller:
                                    _adminService.DeleteSeller();
                                    break;
                                case AdminOperations.DeleteCustomer:
                                    _adminService.DeleteCustomer();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMessage("choice");
                            goto AdminChooseInput;
                        }
                    }
                }
                else
                {
                ChooseWantAdminContinue: Messages.WantToContinueMessage("admin ? y or n");
                    string chooseInput = Console.ReadLine();
                    char choose;
                    bool isSucceeded = char.TryParse(chooseInput, out choose);
                    if (!isSucceeded || !choose.isValidChoose())
                    {
                        Messages.InvalidInputMessage("choose");
                        goto ChooseWantAdminContinue;
                    }
                    if (choose == 'y')
                    {
                        goto ContinueAdminInput;
                    }
                    goto SelectInput;
                }

            }


            else if (select.ToLower() == "seller")
            {
            ContinueSellerInput: var seller = _sellerService.Login();
                if (seller is not null)
                {
                    Messages.WelcomeMessage($" 🤑🤑 {seller.Name}  {seller.Surname}");
                    while (true)
                    {
                        ShowSellerMenu();
                    SellerChooseInput: Messages.InputMessage("choice");
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((SellerOperations)choice)
                            {
                                case SellerOperations.MainMenu:
                                    goto SelectInput;

                                case SellerOperations.ShowSelledProducts:
                                    _sellerService.ShowSelledProducts();
                                    break;
                                case SellerOperations.ShowSelledProductsByDate:
                                    _sellerService.ShowSelledProductsByDate();
                                    break;
                                case SellerOperations.ShowFilteredProducts:
                                    _sellerService.ShowFilteredProducts();
                                    break;
                                case SellerOperations.ShowTotatlIncome:
                                    _sellerService.ShowTotatlIncome();
                                    break;
                                case SellerOperations.CreateProduct:
                                    _sellerService.CreateProduct();
                                    break;
                                case SellerOperations.ChangeProductCount:
                                    _sellerService.ChangeProductCount();
                                    break;
                                case SellerOperations.DeleteProduct:
                                    _sellerService.DeleteProduct();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMessage("choice");
                            goto SellerChooseInput;
                        }
                    }
                }
                else
                {
                ChooseWantSellerContinue: Messages.WantToContinueMessage(" seller ? y or n");
                    string chooseInput = Console.ReadLine();
                    char choose;
                    bool isSucceeded = char.TryParse(chooseInput, out choose);
                    if (!isSucceeded || !choose.isValidChoose())
                    {
                        Messages.InvalidInputMessage("choose");
                        goto ChooseWantSellerContinue;
                    }
                    if (choose == 'y')
                    {
                        goto ContinueSellerInput;
                    }
                    goto SelectInput;
                }
            }


            else if (select.ToLower() == "customer")
            {
            ContinueCustomerInput: var customer = _customerService.Login();
                if (customer is not null)
                {
                    Messages.WelcomeMessage($" 🧟‍♂️  {customer.Name}  {customer.Surname}");
                    while (true)
                    {
                        ShowCustomerMenu();
                    CustomerChooseInput: Messages.InputMessage("choice");
                        string choiceInput = Console.ReadLine();
                        int choice;
                        bool issucceeded = int.TryParse(choiceInput, out choice);
                        if (issucceeded)
                        {
                            switch ((CustomerOperations)choice)
                            {
                                case CustomerOperations.MainMenu:
                                    goto SelectInput;
                                    case CustomerOperations.ShowBuyedProducts:
                                        _customerService.ShowBuyedProducts();
                                    break;
                                case CustomerOperations.ShowOrderByDate:
                                        _customerService.ShowOrderByDate();
                                    break;
                                case CustomerOperations.ShowFilteredProduct:
                                    _customerService.ShowFilteredProduct();
                                    break;
                                    case CustomerOperations.CreateOrder:
                                    _customerService.CreateOrder();
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice");
                                    break;
                            }
                        }
                        else
                        {
                            Messages.InvalidInputMessage("choice");
                            goto CustomerChooseInput;
                        }
                    }
                }
                else
                {
                ChooseWantCustomerContinue: Messages.WantToContinueMessage(" customer ? y or n");
                    string chooseInput = Console.ReadLine();
                    char choose;
                    bool isSucceeded = char.TryParse(chooseInput, out choose);
                    if (!isSucceeded || !choose.isValidChoose())
                    {
                        Messages.InvalidInputMessage("choose");
                        goto ChooseWantCustomerContinue;
                    }
                    if (choose == 'y')
                    {
                        goto ContinueCustomerInput;
                    }
                    goto SelectInput;
                }
            }

            else if (select.ToLower()=="end")
            {
                return;
            }

            else
            {
                Messages.InvalidInputMessage("select");
                goto SelectInput;
            }
        }
    }

    private static void ShowAdminMenu()
    {
        Console.WriteLine("--- Menu ---");
        Console.WriteLine("0 - Main-Menu ");
        Console.WriteLine("1 - ShowAllCustomer ");
        Console.WriteLine("2 - ShowAllSeller ");

        Console.WriteLine("3 - ShowOrdersOrderBy ");
        Console.WriteLine("4 - ShowOrdersByCustomer ");
        Console.WriteLine("5 - ShowOrdersBySeller ");
        Console.WriteLine("6 - ShowOrderByDate ");

        Console.WriteLine("7 - CreateSeller ");
        Console.WriteLine("8 - CreateCustomer ");
        Console.WriteLine("9 - CreateCategory ");
        Console.WriteLine("10 - DeleteSeller ");
        Console.WriteLine("11 - DeleteCustomer ");
    }

    private static void ShowSellerMenu()
    {
        Console.WriteLine("--- Menu ---");
        Console.WriteLine("0 - Main-Menu ");
        Console.WriteLine("1 - ShowSelledProducts ");
        Console.WriteLine("2 - ShowSelledProductsByDate ");
        Console.WriteLine("3 - ShowFilteredProducts ");
        Console.WriteLine("4 - ShowTotatlIncome ");
        Console.WriteLine("5 - CreateProduct ");
        Console.WriteLine("6 - ChangeProductCount ");
        Console.WriteLine("7 - DeleteProduct ");
    }
    private static void ShowCustomerMenu()
    {
        Console.WriteLine("--- Menu ---");
        Console.WriteLine("0 - Main-Menu ");
        Console.WriteLine("1 - ShowBuyedProducts ");
        Console.WriteLine("2 - ShowOrderByDate ");
        Console.WriteLine("3 - ShowFilteredProduct ");
        Console.WriteLine("4 - CreateOrder ");
    }
}
