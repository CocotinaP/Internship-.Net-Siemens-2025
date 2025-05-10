using System.ComponentModel.DataAnnotations;

namespace Internship.NetSiemens2025.domain;

public class Reader : Entity<int>
{
    [Required]
    [MaxLength(15)]
    private string _cnp;
    [Required]
    [MaxLength(100)]
    private string _firstName;
    [Required]
    [MaxLength(100)]
    private string _lastName;
    [Required]
    [MaxLength(20)]
    private string _phoneNumber;
    [Required]
    [MaxLength(30)]
    private string _email;
    
    public Reader(){}

    public Reader(string cnp, string firstName, string lastName, string phoneNumber, string email)
    {
        _cnp = cnp;
        _firstName = firstName;
        _lastName = lastName;
        _phoneNumber = phoneNumber;
        _email = email;
    }

    public Reader(int id, string cnp, string firstName, string lastName, string phoneNumber, string email) : base(id)
    {
        _cnp = cnp;
        _firstName = firstName;
        _lastName = lastName;
        _phoneNumber = phoneNumber;
        _email = email;
    }

    public string Cnp
    {
        get => _cnp;
        set => _cnp = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return base.ToString()
            + "| CNP: " + _cnp
            + "| FIRST NAME: " + _firstName
            + "| LAST NAME: " + _lastName
            + "| PHONE NUMBER: " + _phoneNumber
            + "| EMAIL: " + _email;
    }
}