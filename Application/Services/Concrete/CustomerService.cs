
using Application.Services.Abstract;
using Core.Constants;
using Core.Entities;
using Data.UnitOfWorks.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Application.Services.Concrete;

public class CustomerService : ICustomerService
{
    private readonly UnitOfWork _unitOfWork;
    private Customer _customer;

    public CustomerService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _customer = new();
    }

    public Customer Login()
    {
    EmailInput: Messages.InputMessage("email");
        string email = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(email))
        {
            Messages.InvalidInputMessage("email");
            goto EmailInput;
        }

    PasswordInput: Messages.InputMessage("password");
        string password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Messages.InvalidInputMessage("password");
            goto PasswordInput;
        }
        var existCustomer = _unitOfWork.Customers.GetCustomerByEmail(email);
        if (existCustomer is null)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }
        PasswordHasher<Customer> passwordHasher = new();
        var result = passwordHasher.VerifyHashedPassword(existCustomer, existCustomer.Password, password);
        if (result == PasswordVerificationResult.Failed)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }
        _customer = existCustomer;
        return _customer;
    }

    public void ShowBuyedProducts()
    {
        var existOrders = _unitOfWork.Orders.GetAllOrdersByCustomerId(_customer.Id);
        if (existOrders.Count == 0)
        {
            Messages.NotFoundMessage("buyed order");
            return;
        }
        foreach (var order in existOrders)
        {
            Console.WriteLine($" Product Name : {order.Product.Name}" +
                $" Seller name {order.Seller.Name} product price {order.Product.Price} " +
                $"product count {order.TotalProductCount} total amount : {order.TotalAmount}");
        }
    }
    public void ShowOrderByDate()
    {
     OrderDateInput: Messages.InputMessage("date (dd.MM.yyyy)");
        string dateInput = Console.ReadLine();
        DateTime date;
        bool isSucceeded = DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("date");
            goto OrderDateInput;
        }
        var orders = _unitOfWork.Orders.GetAllOrderByCustomerWithDate(_customer.Id,date);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("any orders");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($" product name : {order.Product.Name} Seller name {order.Seller.Name}" +
                $" Count {order.TotalProductCount}  price {order.Product.Price} create date {order.CreateAt}");
        }
    }

    public void ShowFilteredProduct()
    {
    NameSymbolSect: Messages.InputMessage("name symbol");
        string symbol = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(symbol))
        {
            Messages.InvalidInputMessage("name symbol");
            goto NameSymbolSect;
        }
        var orders = _unitOfWork.Orders.GetOrdersByProductSymbol(_customer.Id,symbol);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("order");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($" product name : {order.Product.Name}  Count : {order.TotalProductCount} " +
                $" Price : {order.Product.Price} total amount : {order.TotalAmount} Seller name : {order.Seller.Name}");
        }

    }

    public void CreateOrder()
    {
        if (_unitOfWork.Products.GetAll().Count == 0)
        {
            Messages.NotFoundMessage("any product");
            return;
        }

    ProductIdSect: Messages.InputMessage("product id");
        foreach (var product in _unitOfWork.Products.GetAll())
        {
            Console.WriteLine($"product id {product.Id} product name {product.Name} product count {product.Count} product price {product.Price}");
        }
        string idInput = Console.ReadLine();
        int id;
        bool isSucceded = int.TryParse(idInput, out id);
        if (!isSucceded || id <= 0)
        {
            Messages.InvalidInputMessage("id");
            goto ProductIdSect;
        }
        var existproduct = _unitOfWork.Products.Get(id);
        if (existproduct is null)
        {
            Messages.NotFoundMessage("product");
            goto ProductIdSect;
        }
    ProductCountSect: Messages.InputMessage("product count");
        string countInput = Console.ReadLine();
        int count;
        isSucceded = int.TryParse(countInput, out count);
        if (!isSucceded || count < 1)
        {
            Messages.InvalidInputMessage("count");
            goto ProductCountSect;
        }
        if (count > existproduct.Count)
        {
            Messages.GreaterInputMessage("stock count", "wanted count");
            goto ProductCountSect;
        }

        decimal totalAmount = count * existproduct.Price;
        Console.WriteLine($"total Amount :{totalAmount} ");

    CompletedInput: Messages.WantToCompleted("product", "buyed");
        string answer = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(answer))
        {
            Messages.InvalidInputMessage("answer");
            goto CompletedInput;
        }
        if (answer.ToLower() == "not")
        {
            Console.WriteLine("order  cancelled");
            return;
        }
        else if (answer.ToLower() != "yes")
        {
            Messages.InvalidInputMessage("answer");
            goto CompletedInput;
        }
        existproduct.Count -= count;

        //if (existproduct.Count == 0)
        //{
        //    _unitOfWork.Products.Delete(existproduct);
        //}

        Order order = new Order
        {
            ProductId = existproduct.Id,
            SellerId = existproduct.SellerId,
            CustomerId = _customer.Id,
            TotalProductCount = count,
            TotalAmount = totalAmount
        };
        _unitOfWork.Products.Update(existproduct);
        _unitOfWork.Orders.Add(order);
        _unitOfWork.Commit();
        Messages.SucceededMessage("product","buyed");
    }

}

