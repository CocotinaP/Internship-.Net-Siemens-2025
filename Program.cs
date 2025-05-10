using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;
using Internship.NetSiemens2025.domain.validator.concrete_validator;
using Internship.NetSiemens2025.persistence;
using Internship.NetSiemens2025.persistence.database;
using Internship.NetSiemens2025.persistence.database.implementation;
using Internship.NetSiemens2025.service;
using Internship.NetSiemens2025.service.implementation;
using Internship.NetSiemens2025.Ui.Console;

namespace Internship.NetSiemens2025;

internal static class Program
{
    private static void Main(string[] args)
    {
        IValidator<Book> bookValidator = new BookValidator();
        IValidator<Reader> readerValidator = new ReaderValidator();
        IValidator<BorrowingItem> borrowingItemValidator = new BorrowingItemValidator();
        IValidator<Borrowing> borrowingValidator = new BorrowingValidator();

        var dbContext = new LibraryDbContext();
        
        IBookRepository bookRepository = new BookDbRepository(bookValidator, dbContext); 
        IReaderRepository readerRepository = new ReaderDbRepository(readerValidator, dbContext);
        IBorrowingItemRepository borrowingItemRepository = new BorrowingItemDbRepository(borrowingItemValidator, dbContext);
        IBorrowingRepository borrowingRepository = new BorrowingDbRepository(borrowingValidator, dbContext);

        IBookService bookService = new BookService(bookRepository);
        IReaderService readerService = new ReaderService(readerRepository);
        IBorrowingItemService borrowingItemService = new BorrowingItemService(borrowingItemRepository);
        IBorrowingService borrowingService = new BorrowingService(borrowingRepository, readerService, borrowingItemService, bookService);
        
        var ui = new ConsoleUi(bookService, borrowingService);
        ui.Show();
        
    }
}