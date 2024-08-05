
using Core.Entities.Base;

namespace Core.Entities;

public abstract  class Person : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}
