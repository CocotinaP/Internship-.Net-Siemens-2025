using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.service;

namespace Internship.NetSiemens2025.Ui.Console;

/// <summary>
/// Console-based UI for managing books and borrowing transactions.
/// Provides a command-driven interface for executing various operations.
/// </summary>
public class ConsoleUi
{
    private readonly Dictionary<string, Action> _commands;
    private IBookService _bookService;
    private IBorrowingService _borrowingService;
    private bool _finish = false;

    /// <summary>
    /// Initializes a new instance of the ConsoleUI class.
    /// </summary>
    /// <param name="bookService">
    /// Instance of IBookService used to manage book-related operations.
    /// </param>
    /// <param name="borrowingService">
    /// Instance of IBorrowingService used to handle borrowing transactions.
    /// </param>
    public ConsoleUi(IBookService bookService, IBorrowingService borrowingService)
    {
        _bookService = bookService;
        _borrowingService = borrowingService;
        
        _commands = new Dictionary<string, Action>()
        {
            {"0", ShowBooks},
            {"1", AddBook},
            {"2", UpdateBook},
            {"3", DeleteBook},
            {"4", FilterBooks},
            {"5", ShowBorrowings},
            {"6", AddBorrowing},
            {"7",ReturnBorrowing},
            {"8", ShowUnreturnedBorrowingsExceededDeadline},
            {"9", GetMostBorrowedBooksCurrentYear},
            {"X", Exit}
        };
    }

    /// <summary>
    /// Get most borrowed books from the current year.
    /// FUNCTIONALITY PROPOSED BY ME.
    /// </summary>
    private void GetMostBorrowedBooksCurrentYear()
    {
        try
        {
            var borrowings = _borrowingService.GetMostBorrowedBooksCurrentYear();

            if (borrowings.Count == 0)
            {
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("No unreturned borrowing exceeded deadline!");
                System.Console.ResetColor();
            }
            else
            {
                borrowings.ForEach(System.Console.WriteLine);
            }
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Show unreturned borrowings exceeded deadline.
    /// FUNCTIONALITY PROPOSED BY ME.
    /// </summary>
    private void ShowUnreturnedBorrowingsExceededDeadline()
    {
        try
        {
            var borrowings = _borrowingService.UnreturnedBorrowingsExceededDeadline();

            if (borrowings.Count == 0)
            {
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("No unreturned borrowing exceeded deadline!");
                System.Console.ResetColor();
            }
            else
            {
                borrowings.ForEach(System.Console.WriteLine);
            }
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Return borrowing.
    /// </summary>
    private void ReturnBorrowing()
    {
        try
        {
            System.Console.WriteLine("Loan ID to be returned:");
            var id = int.Parse(System.Console.ReadLine());
            
            var borrowing = _borrowingService.GetById(id);
            _borrowingService.Return(borrowing);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Loan successfully returned!");
            System.Console.ResetColor();
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Show all borrowings.
    /// FUNCTIONALITY PROPOSED BY ME.
    /// </summary>
    private void ShowBorrowings()
    {
        try
        {
            System.Console.ForegroundColor = ConsoleColor.DarkMagenta;
            System.Console.WriteLine("Type: all/ returned/ not returned:");
            System.Console.ResetColor();
            var type = System.Console.ReadLine();
            
            List<Borrowing>? borrowings = null;

            switch (type)
            {
                case "all":
                    borrowings = _borrowingService.GetAll();
                    break;
                case "returned":
                    borrowings = _borrowingService.FilterReturnedBorrowing(true);
                    break;
                case "not returned":
                    borrowings = _borrowingService.FilterReturnedBorrowing(false);
                    break;
                default:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(type + " is not a valid borrowing type!");
                    System.Console.ResetColor();
                    break;
            }

            if (borrowings == null) return;
            if (borrowings.Count == 0)
            {
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("There are no borrowings.");
                System.Console.ResetColor();
            }
            else
            {
                borrowings.ForEach(System.Console.WriteLine);
            }
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Close the application.
    /// </summary>
    private void Exit()
    {
        System.Console.WriteLine("Exit.");
        _finish = true;
    }

    /// <summary>
    /// Add borrowing.
    /// </summary>
    private void AddBorrowing()
    {
        List<BorrowingItem> borrowingItems = new List<BorrowingItem>();
        try
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Reader data:");
            System.Console.ResetColor();
            
            System.Console.WriteLine("CNP:");
            var readerCnp = System.Console.ReadLine();
            System.Console.WriteLine("First Name:");
            var readerFirstName = System.Console.ReadLine();
            System.Console.WriteLine("Last Name:");
            var readerLastName = System.Console.ReadLine();
            System.Console.WriteLine("Phone Number:");
            var readerPhoneNumber = System.Console.ReadLine();
            System.Console.WriteLine("Email:");
            var readerEmail = System.Console.ReadLine();
            
            var reader = new Reader(readerCnp, readerFirstName, readerLastName, readerPhoneNumber, readerEmail);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Books Data:");
            System.Console.ResetColor();

            System.Console.ForegroundColor = ConsoleColor.DarkMagenta;
            System.Console.WriteLine("When you want to add another book, type '+', else, type 'x':");
            System.Console.ResetColor();
            String newAdd;
            do
            {
                System.Console.WriteLine("ID:");
                var id = int.Parse(System.Console.ReadLine());
                System.Console.WriteLine("Quantity:");
                var quantity = int.Parse(System.Console.ReadLine());
                System.Console.WriteLine("New add:");
                newAdd = System.Console.ReadLine();

                var book = _bookService.GetById(id);
                var borrowingItem = new BorrowingItem(book, quantity);
                borrowingItems.Add(borrowingItem);
            }while(newAdd != "x");
            
            Borrowing borrowing = new Borrowing(reader, borrowingItems);
            
            _borrowingService.Add(borrowing);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Borrowing added.");
            System.Console.ResetColor();
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Filter books.
    /// </summary>
    private void FilterBooks()
    {
        try
        {
            System.Console.WriteLine("Fill in the filter criteria values and enter the values for each criterion separated by '|':");
            System.Console.WriteLine("Title:");
            var title = System.Console.ReadLine();
            System.Console.WriteLine("Author:");
            var author = System.Console.ReadLine();
            System.Console.WriteLine("Category:");
            var category = System.Console.ReadLine();
            System.Console.WriteLine("Genre:");
            var genre = System.Console.ReadLine();
            
            var titles = string.IsNullOrEmpty(title) ? [] : title.Split('|').ToList();
            var authors = string.IsNullOrEmpty(author) ? [] : author.Split('|').ToList();
            var categories = string.IsNullOrEmpty(category) ? [] : category.Split('|').ToList();
            var genres = string.IsNullOrEmpty(genre) ? [] : genre.Split('|').ToList();
            
            var books = _bookService.FilterBooks(titles, authors, genres, categories);

            if (books.Count > 0)
            {
                books.ForEach(System.Console.WriteLine);
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("No books found.");
                System.Console.ResetColor();
            }
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Delete book.
    /// </summary>
    private void DeleteBook()
    {
        try
        {
            System.Console.WriteLine("Necessary data: ");
            System.Console.WriteLine("ID of the book to be deleted: ");
            var id = int.Parse(System.Console.ReadLine() ?? string.Empty);
           
            _bookService.Delete(id);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Book deleted.");
            System.Console.ResetColor();
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Update book.
    /// </summary>
    private void UpdateBook()
    {
        try
        {
            System.Console.WriteLine("Necessary data: ");
            System.Console.WriteLine("ID of the book to be modified: ");
            var id = int.Parse(System.Console.ReadLine() ?? string.Empty);
            System.Console.WriteLine("Title: ");
            var title = System.Console.ReadLine();
            System.Console.WriteLine("Author: ");
            var author = System.Console.ReadLine();
            System.Console.WriteLine("Quantity: ");
            var quantity = int.Parse(System.Console.ReadLine() ?? string.Empty);
            System.Console.WriteLine("Category: ");
            var category = System.Console.ReadLine();
            System.Console.WriteLine("Genre: ");
            var genre = System.Console.ReadLine();

            var oldBook = _bookService.GetById(id);
            var book = new Book(id, title, author, quantity, category, genre);
            book.AvailablilityNumber = oldBook.AvailablilityNumber + book.Quantity - oldBook.Quantity;
            _bookService.Update(book);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Book updated.");
            System.Console.ResetColor();
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Add book.
    /// </summary>
    private void AddBook()
    {
        try
        {
            System.Console.WriteLine("Necessary data: ");
            System.Console.WriteLine("Title: ");
            var title = System.Console.ReadLine();
            System.Console.WriteLine("Author: ");
            var author = System.Console.ReadLine();
            System.Console.WriteLine("Quantity: ");
            var quantity = int.Parse(System.Console.ReadLine() ?? string.Empty);
            System.Console.WriteLine("Category: ");
            var category = System.Console.ReadLine();
            System.Console.WriteLine("Genre: ");
            var genre = System.Console.ReadLine();

            var book = new Book(title, author, quantity, category, genre);
            _bookService.Add(book);
            
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Book added.");
            System.Console.ResetColor();
        }
        catch (Exception exception)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(exception.Message);
            System.Console.ResetColor();
        }
    }

    /// <summary>
    /// Show all books.
    /// </summary>
    private void ShowBooks()
    {
        var books = _bookService.GetAll();
        if (books.Count == 0)
        {
            System.Console.WriteLine("There are no books!");
        }
        books.ForEach(System.Console.WriteLine);
    }

    /// <summary>
    /// Displays the application menu.
    /// </summary>
    private void Menu()
    {
        System.Console.ForegroundColor = ConsoleColor.DarkMagenta;
        System.Console.WriteLine("Options:");
        System.Console.ResetColor();
        foreach (var command in _commands)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkMagenta;
            System.Console.Write($"{command.Key}. ");
            System.Console.ResetColor();
            
            System.Console.WriteLine($"{command.Value.Method.Name}");
        }
    }

    /// <summary>
    /// Start the application.
    /// </summary>
    public void Show()
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (!_finish)
        {
            Menu();
            System.Console.WriteLine("Choose a number of the desired option:");
            var command = System.Console.ReadLine();

            if (command != null && _commands.TryGetValue(command, out var action))
            {
                action.Invoke();
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine("Invalid option. Try again.");
                System.Console.ResetColor();
            }
        }
    }
}