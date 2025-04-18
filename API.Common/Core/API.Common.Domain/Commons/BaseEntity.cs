namespace API.Common.Domain.Commons
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; } 
        virtual public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
