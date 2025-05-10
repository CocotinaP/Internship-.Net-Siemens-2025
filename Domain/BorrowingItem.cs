namespace Internship.NetSiemens2025.domain;

public class BorrowingItem: Entity<int>
{
    private Book _book;
    private int _quantity;
    
    public BorrowingItem(){}

    public BorrowingItem(Book book, int quantity)
    {
        _book = book;
        _quantity = quantity;
    }

    public BorrowingItem(int id, Book book, int quantity) : base(id)
    {
        _book = book;
        _quantity = quantity;
    }

    public Book Book
    {
        get => _book;
        set => _book = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
    }

    public override string ToString()
    {
        return base.ToString()
            + "|BOOK: " + Book
            + "| QUANTITY: " + _quantity;
    }
}