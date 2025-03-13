using DataLayer.Base;

namespace DataLayer.DbObject;

public class Genre : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Song>? Songs { get; set; }
    
}