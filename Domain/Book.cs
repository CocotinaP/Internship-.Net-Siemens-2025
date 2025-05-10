using System.ComponentModel.DataAnnotations;
using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.domain;

public class Book : Entity<int>
{
    [Required]
    [MaxLength(50)]
    private string _title;
    [MaxLength(50)]
    private string _author;
    [Required]
    private int _quantity;
    [Required]
    [MaxLength(50)]
    private string _category;
    [Required]
    [MaxLength(50)]
    private string _genre;

    private int _availabilityNumber;
    
    public Book(){}

    public Book(string title, string author, int quantity, string category, string genre)
    {
        _title = title;
        _author = author;
        _quantity = quantity;
        _category = category;
        _genre = genre;
        _availabilityNumber = quantity;
    }

    public Book(int id, string title, string author, int quantity, string category, string genre) : base(id)
    {
        _title = title;
        _author = author;
        _quantity = quantity;
        _category = category;
        _genre = genre;
    }

    public string Title
    {
        get => _title;
        set => _title = value;
    }

    public string Author
    {
        get => _author;
        set => _author = value;
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
    }

    public string Category
    {
        get => _category;
        set => _category = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Genre
    {
        get => _genre;
        set => _genre = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int AvailablilityNumber
    {
        get => _availabilityNumber;
        set => _availabilityNumber = value;
    }

    public override string ToString()
    {
        return base.ToString()
            + "| TITLE: " + _title
            + "| AUTHOR: " + _author
            + "| QUANTITY: " + _quantity
            + "| CATEGORY: " + _category
            + "| GENRE: " + _genre
            + "| AVAILABILITY NUMBER: " + _availabilityNumber;
    }
}