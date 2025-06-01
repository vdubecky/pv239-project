using SQLite;

namespace pv239_project.Entities;

public class EntityBase
{
    [PrimaryKey]
    public Guid Id { get; set; }
}