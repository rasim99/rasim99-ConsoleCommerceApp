
namespace Core.Entities.Base;

public abstract class PersonalInfo  :Person
{
    public string PhoneNumber { get; set; }
    public string Pin { get; set; }
    public string SerialNumber { get; set; }
}
