
using Core.Entities;
using Data.Repistories.Base;

namespace Data.Repistories.Abstract
{
    public interface ICategoryRepistory : IRepistory<Category>
    {
        public Category GetByName(string name);

    }
}
