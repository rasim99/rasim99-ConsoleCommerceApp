
using Core.Entities;
using Data.Repistories.Base;

namespace Data.Repistories.Abstract;

public interface IProductRepistory :IRepistory<Product>
{
    Product GetExistProduct(string name, int id);
    List<Product> GetAllBySeller(int id);
    List<Product> GetAllByName(string name);
    List<Product> FilteredProducts(int sellerId, string nameCharacter);
}
