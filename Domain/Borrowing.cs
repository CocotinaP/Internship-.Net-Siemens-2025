namespace Internship.NetSiemens2025.domain;

public class Borrowing : Entity<int>
{
    private Reader _reader;
    private List<BorrowingItem> _borrowingItems;
    private DateTime _borrowingDate;
    private DateTime? _returningDate;
    
    public Borrowing(){}

    public Borrowing(Reader reader, List<BorrowingItem> borrowingItems)
    {
        _reader = reader;
        _borrowingItems = borrowingItems;
    }

    public Borrowing(int id, Reader reader, DateTime borrowingDate, List<BorrowingItem> borrowingItems) : base(id)
    {
        _reader = reader;
        _borrowingDate = borrowingDate;
        _borrowingItems = borrowingItems;
    }

    public Reader Reader
    {
        get => _reader;
        set => _reader = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<BorrowingItem> BorrowingItems => _borrowingItems;

    public DateTime BorrowingDate
    {
        get => _borrowingDate;
        set => _borrowingDate = value;
    }

    public DateTime? ReturningDate
    {
        get => _returningDate;
        set => _returningDate = value;
    }

    public override string ToString()
    {
        return "\uD83D\uDD11 " + base.ToString()
            + "\n\uD83D\uDCC5 BORROWING DATE: " + _borrowingDate
            + "\n\uD83D\uDCC5 RETURNING DATE: " + _returningDate
            + "\n\uD83D\uDCDA BORROWING ITEMS: \n\t\uD83D\uDCD6 " + string.Join("\n\t\uD83D\uDCD6 ", _borrowingItems)
            + "\n\uD83E\uDDD1 READER: " + _reader
            +"\n";
    }
}