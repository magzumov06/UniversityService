using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IBookServices
{
    bool AddBook(Book book);
    List<Book> GetAllBooks();
    bool UpdateBook(int id, Book book);
    bool DeleteBook(int id);
}