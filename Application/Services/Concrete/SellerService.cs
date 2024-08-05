using Application.Services.Abstract;
using Core.Constants;
using Core.Entities;
using Data.UnitOfWorks.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
namespace Application.Services.Concrete;

public class SellerService : ISellerService
{
    private readonly UnitOfWork _unitOfWork;
    private Seller _seller;

    public SellerService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _seller = null;
    }

    public Seller Login()
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
        var existSeller = _unitOfWork.Sellers.GetSellerByEmail(email);
        if (existSeller is null)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }
        PasswordHasher<Seller> passwordHasher = new();
        var result = passwordHasher.VerifyHashedPassword(existSeller, existSeller.Password, password);
        if (result == PasswordVerificationResult.Failed)
        {
            Messages.WrongInputMessage("email or password");
            return null;
        }
        _seller = existSeller;
        return _seller;
    }
    public void ShowSelledProducts()
    {

        var orders = _unitOfWork.Orders.GetAllOrderBySeller(_seller.Id);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("this seller-s any product");
            return;
        }
        foreach (var order in orders)
        {
            Console.WriteLine($"Product name : {order.Product.Name} selled product count : {order.TotalProductCount}" +
                $" Product price : {order.Product.Price} Customer Name : {order.Customer.Name}" +
                $"  Buyed date : {order.CreateAt.ToString("dd.MM.yyyy")}");
        }

    }

    public void ShowSelledProductsByDate()
    {
        if (_unitOfWork.Orders.GetAllOrderBySeller(_seller.Id).Count==0)
        {
            Messages.NotFoundMessage("seller-s any selled product");
            return;
        }
    DateInput: Messages.InputMessage("date ( dd.MM.yyyy )");
        string dateInput = Console.ReadLine();
        DateTime date;
        bool isSucceeded = DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        if (!isSucceeded)
        {
            Messages.InvalidInputMessage("date");
            goto DateInput;
        }
        var orders = _unitOfWork.Orders.GetAllSelledProductByDate(_seller.Id, date);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage("any selled product");
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine($"Product name : {order.Product.Name} selled product count : {order.TotalProductCount}" +
                $" Product price : {order.Product.Price} Customer Name : {order.Customer.Name}" +
                $"  Buyed date : {order.CreateAt.ToString("dd.MM.yyyy")}");
        }


    }
 
    public void ShowFilteredProducts()
    {
        if (_unitOfWork.Products.GetAllBySeller(_seller.Id).Count==0)
        {
            Messages.NotFoundMessage("seller -s any product");
            return;
        }
        NameCharacterSect: Messages.InputMessage("name character");
        string nameCharacter=Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nameCharacter))
        {
            Messages.InvalidInputMessage("name character");
            goto NameCharacterSect;
        }
        var products=_unitOfWork.Products.FilteredProducts(_seller.Id, nameCharacter);
        if (products.Count==0)
        {
            Messages.NotFoundMessage("product");
            return;
        }
        foreach (var product in products)
        {
            Console.WriteLine($"Product name : {product.Name}   count : {product.Count} price : {product.Price}" );
             
        }
    }

    public void ShowTotatlIncome()
    {
        
        var orders=_unitOfWork.Orders.GetAllOrderBySeller( _seller.Id);
        if (orders.Count==0)
        {
            Messages.NotFoundMessage(" any selled product");
            return;
        }
        decimal totalIncome = 0;
        foreach (var order in orders)
        {
            totalIncome += (order.Product.Price * order.TotalProductCount);
        }
        Console.WriteLine($"Total Income : {totalIncome}");
    }
    
    
    public void CreateProduct()
    {
        if (_unitOfWork.Categories.GetAll().Count==0)
        {
            Messages.NotFoundMessage("cannot creating product ! any category");
            return;
        }
    ProductCategoryInput: Messages.InputMessage(" category id");
        foreach (var category in _unitOfWork.Categories.GetAll())
        {
            Console.WriteLine($"Id : {category.Id}  Name : {category.Name}");
        }

        string categoryIdInput = Console.ReadLine();
        int categoryId;
        bool isSucceded = int.TryParse(categoryIdInput, out categoryId);
        if (!isSucceded || categoryId<=0)
        {
            Messages.InvalidInputMessage(" category id");
            goto ProductCategoryInput;
        }
        var existCategory=_unitOfWork.Categories.Get(categoryId);
        if (existCategory is null)
        {
            Messages.NotFoundMessage(" category");
            goto ProductCategoryInput;
        }


    ProductNameInput: Messages.InputMessage("product name");
        string productName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(productName))
        {
            Messages.InvalidInputMessage("productName");
            goto ProductNameInput;
        }

        var existPrdouct = _unitOfWork.Products.GetExistProduct(productName, _seller.Id);
        if (existPrdouct is not null)
        {
            Messages.AlreadyExistMessage($" seller - s  {productName} ");
            return;
        }


    ProductPriceInput: Messages.InputMessage("product price");
        string priceInput = Console.ReadLine();
        decimal price;
         isSucceded = decimal.TryParse(priceInput, out price);
        if (!isSucceded)
        {
            Messages.InvalidInputMessage(" price");
            goto ProductPriceInput;
        }     
        if (price <= 0)
        {
            Messages.GreaterInputMessage("product price", "0");
            goto ProductPriceInput;
        }


    ProductCountInput: Messages.InputMessage("product count");
        string countInput = Console.ReadLine();
        int count;
         isSucceded = int.TryParse(countInput, out count);
        if (!isSucceded)
        {
            Messages.InvalidInputMessage(" count");
            goto ProductCountInput;
        }

        if (count <1) 
        {
            Messages.GreaterInputMessage("product count","0");
            goto ProductCountInput;
        }

        Product product = new Product
        {
            Name= productName,
            Price= price,
            Count= count,
            CategoryId= categoryId,
            SellerId=_seller.Id  
        };

        _unitOfWork.Products.Add(product);
        _unitOfWork.Commit();
        Messages.SucceededMessage("product","added");
    }

    public void ChangeProductCount()
    {
        if (_unitOfWork.Products.GetAllBySeller(_seller.Id).Count==0)
        {
            Messages.NotFoundMessage(" any product");
            return;
        }
    ProductIdSection: Messages.InputMessage("product id");
        foreach (var product in _unitOfWork.Products.GetAllBySeller(_seller.Id))
        {
            Console.WriteLine($"Id : {product.Id}  Product Name : {product.Name} Product count {product.Count}");
        }
        string productIdInput= Console.ReadLine();
        int productId;
        bool isSucceeded = int.TryParse(productIdInput, out productId);
        if (!isSucceeded || productId<=0)
        {
            Messages.InvalidInputMessage("product id");
            goto ProductIdSection;
        }
        var existProduct = _unitOfWork.Products.Get(productId);
        if (existProduct is null)
        {
            Messages.NotFoundMessage("product");
            return;
        }

        ProductNewCountSection: Messages.InputMessage("product new count");
        string newCountInput=Console.ReadLine();
        int newCount;
        isSucceeded = int.TryParse(newCountInput, out newCount);
        if (!isSucceeded || newCount<1)
        {
            Messages.InvalidInputMessage("new count");
            goto ProductNewCountSection;
        }
        existProduct.Count = newCount;
        _unitOfWork.Products.Update(existProduct);
        _unitOfWork.Commit();
        Messages.SucceededMessage("product count","updated");
    }

    public void DeleteProduct()
    {
        if (_unitOfWork.Products.GetAllBySeller(_seller.Id).Count == 0)
        {
            Messages.NotFoundMessage(" any product");
            return;
        }
    ProductIdSection: Messages.InputMessage("product id");
        foreach (var product in _unitOfWork.Products.GetAllBySeller(_seller.Id))
        {
            Console.WriteLine($"Id : {product.Id} product name : {product.Name} product count : {product.Count}");
        }
        string productIdInput = Console.ReadLine();
        int productId;
        bool isSucceeded = int.TryParse(productIdInput, out productId);
        if (!isSucceeded || productId<=0)
        {
            Messages.InvalidInputMessage("product id");
            goto ProductIdSection;
        }
        var existProduct = _unitOfWork.Products.Get(productId);
        if (existProduct is null)
        {
            Messages.NotFoundMessage("product");
            return;
        }
        _unitOfWork.Products.Delete(existProduct);
        _unitOfWork.Commit();
        Messages.SucceededMessage("product","deleted");
    }


}


