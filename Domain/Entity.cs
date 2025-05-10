namespace Internship.NetSiemens2025.domain;

public class Entity<TId>
{
    private TId? _id;
    
    public Entity(){}

    public Entity(TId id)
    {
        _id = id;
    }

    public TId? Id
    {
        get => _id;
        set => _id = value;
    }

    public override string ToString()
    {
        return "ID: "  + Id?.ToString();
    }
}