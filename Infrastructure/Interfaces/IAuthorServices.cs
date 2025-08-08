using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IAuthorServices
{
    bool AddAuthor(Author author);
    List<Author> GetAllAuthors();
    bool UpdateAuthor( int id,Author author);
    bool DeleteAuthor(int id);
}