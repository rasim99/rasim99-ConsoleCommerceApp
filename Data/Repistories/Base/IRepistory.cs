using Core.Entities.Base;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
namespace Data.Repistories.Base;

public interface IRepistory<T> where T : BaseEntity
{
    List<T> GetAll();
    T Get(int id);

    void Add(T entity);
   void Update(T entity);

    void Delete(T entity);
}
