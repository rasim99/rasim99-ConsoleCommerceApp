

namespace Core.Entities.Base;

public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? ModifyAt { get; set; }
}
