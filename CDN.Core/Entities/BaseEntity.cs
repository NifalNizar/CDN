namespace CDN.Core.Entities;
public class BaseEntity
{
    public int Id { get; set; }

    public int Status { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
