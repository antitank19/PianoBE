using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 
namespace DataLayer.Base
{
    public abstract class BaseEntity
    {
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
