
using Application.Services.Abstract;
using Core.Constants;
using Core.Entities;
using Core.Extensions;
using Data.UnitOfWorks.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Application.Services.Concrete;

public class AdminService : IAdminService
{
    private UnitOfWork _unitOfWork;
    public AdminService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Admin Login()
    {
    AdminEmailInput:  Messages.InputMessage("Admin email");
        string email=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email))
        {
            Messages.InvalidInputMessage("admin email");
            goto AdminEmailInput;
        }

            AdminPasswordInput:  Messages.InputMessage("Admin password");
        string password=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password)|| !password.isValidPassword())
        {
            Messages.InvalidInputMessage("admin password");
            goto AdminPasswordInput;
        }


        var admin=_unitOfWork.Admins.GetAdminByEmail(email);
        if (admin is null)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }

        PasswordHasher<Admin> passwordHasher = new();
        var result = passwordHasher.VerifyHashedPassword(admin,admin.Password,password);
        if (result==PasswordVerificationResult.Failed)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }
        Messages.SucceededMessage("admin","loggined");
        return admin;
    }
    public void ShowAllCustomer()
    {
        if (_unitOfWork.Customers.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any customer");
            return;
        }

        foreach (var customer in _unitOfWork.Customers.GetAll())
        {
            Console.WriteLine($"Id : {customer.Id} Name : {customer.Name} Surname : {customer.Surname}" +
                $" Email: {customer.Email}PhoneNumber : {customer.PhoneNumber}");
        }
    }
    public void ShowAllSeller()
    {
        if (_unitOfWork.Sellers.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any seller");
            return;
        }

        foreach (var seller in _unitOfWork.Sellers.GetAll())
        {
            Console.WriteLine($"Id : {seller.Id} Name : {seller.Name} Surname : {seller.Surname}" +
                $" Email: {seller.Email}PhoneNumber : {seller.PhoneNumber}");
        }
    }
    public void ShowOrdersOrderBy()
    {
        var orders = _unitOfWork.Orders.GetAllOrdersOrderByDesc();
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("any order");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($"Date : {order.CreateAt}  order id : {order.Id}  Product name : {order.Product.Name}" +
                $" Customer name : {order.Customer.Name}  Seller name{order.Seller.Name}");
        }

    }
    public void ShowOrderByDate()
    {
        if (_unitOfWork.Orders.GetAll().Count==0)
        {
            Messages.NotFoundMessage("any order");
            return;
        }
       DateInput: Messages.InputMessage(" Date (dd.MM.yyyy)");
        string dateInput=Console.ReadLine();
        DateTime date;
        bool isSucceeded=DateTime.TryParseExact(dateInput,"dd.MM.yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out date);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("date");
            goto DateInput;
        }
        var orders=_unitOfWork.Orders.GetAllOrdersByDate(date);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("order");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($"order id : {order.Id}  Order Date : {order.CreateAt}" +
                $" product Name: {order.Product.Name} product price :{order.Product.Price}");
        }
    }
    public void ShowOrderByCustomer()
    {
        if (_unitOfWork.Customers.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any customer");
            return;
        } 
        if (_unitOfWork.Orders.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any order");
            return;
        }
    IdInput: Messages.InputMessage("id");
        ShowAllCustomer();
        string idInput = Console.ReadLine();
        int id;
        bool isSucceeded = int.TryParse(idInput, out id);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("id");
            goto IdInput;
        }
        var customer = _unitOfWork.Customers.Get(id);
        if (customer is null)
        {
            Messages.NotFoundMessage("customer");
            goto IdInput;
        }
        var ordersOfCustomer = _unitOfWork.Orders.GetAllOrderByCustomer(id);
        if (ordersOfCustomer.Count==0)
        {
            Messages.NotFoundMessage($"{customer} not buy any product");
            return;
        }

        
        foreach (var order in ordersOfCustomer)
        {
            Console.WriteLine($" OrderId : {order.Id} Sellername  :  {order.Seller.Name}   " +
                $" ProductName:  {order.Product.Name} total amount :  {order.TotalAmount}");
        }
    }
    public void ShowOrderBySeller()
    {
        if (_unitOfWork.Sellers.GetAll().Count==0 )
        {
            Messages.NotFoundMessage("any seller");
            return;
        }
        if (_unitOfWork.Orders.GetAll().Count==0 )
        {
            Messages.NotFoundMessage("any order");
            return;
        }
    IdInput: Messages.InputMessage("id");
        ShowAllSeller();
        string idInput = Console.ReadLine();
        int id;
        bool isSucceeded = int.TryParse(idInput, out id);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("id");
            goto IdInput;
        }
        var seller = _unitOfWork.Sellers.Get(id);
        if (seller is null)
        {
            Messages.NotFoundMessage("seller");
            goto IdInput;
        }
        var ordersOfSeller = _unitOfWork.Orders.GetAllOrderByCustomer(id);
        if (ordersOfSeller.Count == 0)
        {
            Messages.NotFoundMessage($"{seller} cannot sell any product");
            return;
        }

        foreach (var order in ordersOfSeller)
        {
            Console.WriteLine($"OrderId : {order.Id} Customer name : {order.Customer.Name} " +
                $" ProductName:  {order.Product.Name} count {order.TotalProductCount} total amount {order.TotalAmount}");
        }
    }

    public void CreateSeller()
    {
       SellerNameInput: Messages.InputMessage("seller name");
        string name=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) 
        {
            Messages.InvalidInputMessage("name");
            goto SellerNameInput;
        } 


    SellerSurnameInput: Messages.InputMessage("seller surname");
        string surname=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname)) 
        {
            Messages.InvalidInputMessage("surname");
            goto SellerSurnameInput;
        }
    

       SellerSerialNumberlInput: Messages.InputMessage("seller serialNumber example AA12345 (A-F + digits not startwith 0)");
        string serialNumber=Console.ReadLine();
        if (!serialNumber.isValidSerialNumber()) 
        {
            Messages.InvalidInputMessage("serialNumber");
            goto SellerSerialNumberlInput;
        }
        if (_unitOfWork.Sellers.GetExistSeller(serialNumber) is not null||_unitOfWork.Customers.GetExistCustomer(serialNumber)is not null)
        {
            Messages.AlreadyExistMessage("serialNumber");
            goto SellerSerialNumberlInput;
        } 
    
    SellerPinInput: Messages.InputMessage("seller pin example 1ADS56T startWith 1-9");
        string pin=Console.ReadLine();
        if (!pin.isValidPin()) 
        {
            Messages.InvalidInputMessage("pin");
            goto SellerPinInput;
        }
        if (_unitOfWork.Sellers.GetExistSeller(pin) is not null||_unitOfWork.Customers.GetExistCustomer(pin)is not null)
        {
            Messages.AlreadyExistMessage("pin");
            goto SellerPinInput;
        } 
    

    SellerPhoneNumberInput: Messages.InputMessage("seller phonenumber ( +994 xx xxx xx xx /  0xx xxx xx xx)");
        string phonenumber = Console.ReadLine();
        if (!phonenumber.isValidPhoneNumber() ) 
        {
            Messages.InvalidInputMessage("phoneNumber");
            goto SellerPhoneNumberInput;
        }
        if (_unitOfWork.Sellers.GetExistSeller(phonenumber) is not null||_unitOfWork.Customers.GetExistCustomer(phonenumber) is not null)
        {
            Messages.AlreadyExistMessage("phoneNumber");
            goto SellerPhoneNumberInput;
        }

      

     SellerEmailInput: Messages.InputMessage("seller email (example : seller@gmail.com)");
        string email = Console.ReadLine();
        if (!email.isValidEmail())
        {
            Messages.InvalidInputMessage("email");
            goto SellerEmailInput;
        }
        if (_unitOfWork.Sellers.GetExistSeller(email) is not null || _unitOfWork.Customers.GetExistCustomer(email) is not null)
        {
            Messages.AlreadyExistMessage("email");
            goto SellerEmailInput;
        }

    SellerPasswordInput: Messages.InputMessage("seller paswword");

        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password) || !password.isValidPassword())
        {
            Messages.InvalidInputMessage("password");
            goto SellerPasswordInput;
        }
    
        Seller seller = new Seller
        {
            Name =name,
            Surname = surname,
            Email =email,
            PhoneNumber =phonenumber,
            Pin = pin,
            SerialNumber = serialNumber
        };
        PasswordHasher<Seller> passwordHasher = new();
       seller.Password= passwordHasher.HashPassword(seller, password);

        _unitOfWork.Sellers.Add(seller);
        _unitOfWork.Commit();
        Messages.SucceededMessage("seller","added");
    }

    public void CreateCustomer()
    {
    CustomerNameInput: Messages.InputMessage("customer name");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Messages.InvalidInputMessage("name");
            goto CustomerNameInput;
        }


    CustomerSurnameInput: Messages.InputMessage("customer surname");
        string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname))
        {
            Messages.InvalidInputMessage("surname");
            goto CustomerSurnameInput;
        }


    CustomerSerialNumberlInput: Messages.InputMessage("customer serialNumber example AA12345 (A-F + digits not startwith 0)");
        string serialNumber = Console.ReadLine();
        if (!serialNumber.isValidSerialNumber())
        {
            Messages.InvalidInputMessage("serialNumber");
            goto CustomerSerialNumberlInput;
        }
        if ( _unitOfWork.Customers.GetExistCustomer(serialNumber) is not null || _unitOfWork.Sellers.GetExistSeller(serialNumber) is not null)
        {
            Messages.AlreadyExistMessage("serialNumber");
            goto CustomerSerialNumberlInput;
        }

    CustomerPinInput: Messages.InputMessage("customer pin example 1ADS56T startWith 1-9");
        string pin = Console.ReadLine();
        if (!pin.isValidPin())
        {
            Messages.InvalidInputMessage("pin");
            goto CustomerPinInput;
        }
        if ( _unitOfWork.Customers.GetExistCustomer(pin) is not null|| _unitOfWork.Sellers.GetExistSeller(pin) is not null)
        {
            Messages.AlreadyExistMessage("pin");
            goto CustomerPinInput;
        }


    CustomerPhoneNumberInput: Messages.InputMessage("customer phonenumber ( +994 xx xxx xx xx /  0xx xxx xx xx)");
        string phonenumber = Console.ReadLine();
        if (!phonenumber.isValidPhoneNumber())
        {
            Messages.InvalidInputMessage("phoneNumber");
            goto CustomerPhoneNumberInput;
        }
        if ( _unitOfWork.Customers.GetExistCustomer(phonenumber) is not null || _unitOfWork.Sellers.GetExistSeller(phonenumber) is not null)
        {
            Messages.AlreadyExistMessage("phoneNumber");
            goto CustomerPhoneNumberInput;
        }

    CustomerEmailInput: Messages.InputMessage("customer email");
        string email = Console.ReadLine();
        if (!email.isValidEmail())
        {
            Messages.InvalidInputMessage("email");
            goto CustomerEmailInput;
        }
        if ( _unitOfWork.Customers.GetExistCustomer(email) is not null || _unitOfWork.Sellers.GetExistSeller(email) is not null)
        {
            Messages.AlreadyExistMessage("email");
            goto CustomerEmailInput;
        }

    CustomerPasswordInput: Messages.InputMessage("customer paswword");

        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password) || !password.isValidPassword())
        {
            Messages.InvalidInputMessage("password");
            goto CustomerPasswordInput;
        }

        Customer customer = new Customer
        {
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phonenumber,
            Pin = pin,
            SerialNumber = serialNumber
        };
        PasswordHasher<Customer> passwordHasher = new();
        customer.Password = passwordHasher.HashPassword(customer, password);

        _unitOfWork.Customers.Add(customer);
        _unitOfWork.Commit();
        Messages.SucceededMessage("customer", "added");
    }

    public void CreateCategory()
    {
       CategoryNameInput:  Messages.InputMessage("category name");
        string name=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Messages.InvalidInputMessage("name");
            goto CategoryNameInput;
        }
        var existcategory = _unitOfWork.Categories.GetByName(name);
        if (existcategory != null)
        {
            Messages.AlreadyExistMessage(name);
            return ;
        }
        Category category = new Category
        {
            Name = name,
        };
        _unitOfWork.Categories.Add(category);
        _unitOfWork.Commit();
        Messages.SucceededMessage("category", "added");

    }
    public void DeleteSeller()
    {
        if (_unitOfWork.Sellers.GetAll().Count==0)
        {
            Messages.NotFoundMessage("any seller");
            return;
        }
       DeleteSellerIdInput: Messages.InputMessage(" seller id");
        foreach (var seller in _unitOfWork.Sellers.GetAll())
        {
            Console.WriteLine($"Id : {seller.Id} Name : {seller.Name} Surname : {seller.Surname}");
        }
        string idInput= Console.ReadLine();
        int id;
        bool isSucceeded=int.TryParse(idInput, out id); 
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("id");
            goto DeleteSellerIdInput;
        }
        var existSeller=_unitOfWork.Sellers.Get(id);
        if (existSeller is null)
        {
            Messages.NotFoundMessage("seller");
            return ;
        }
        _unitOfWork.Sellers.Delete(existSeller);
        _unitOfWork.Commit();
        Messages.SucceededMessage("seller","deleted");
    }

    public void DeleteCustomer()
    {
        if (_unitOfWork.Customers.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any customer");
            return;
        }
    DeleteCustomerIdInput: Messages.InputMessage(" customer id");
        foreach (var customer in _unitOfWork.Customers.GetAll())
        {
            Console.WriteLine($"Id : {customer.Id} Name : {customer.Name} Surname :  {customer.Surname}");
        }
        string idInput = Console.ReadLine();
        int id;
        bool isSucceeded = int.TryParse(idInput, out id);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("id");
            goto DeleteCustomerIdInput;
        }
        var existCustomer = _unitOfWork.Customers.Get(id);
        if (existCustomer is null)
        {
            Messages.NotFoundMessage("customer");
            return;
        }
        _unitOfWork.Customers.Delete(existCustomer);
        _unitOfWork.Commit();
        Messages.SucceededMessage("customer", "deleted");
    }
}
