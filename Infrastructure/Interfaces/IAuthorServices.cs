using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IAuthorServices
{
    void AddAuthor(Author author);
    List<Author> GetAllAuthors();
    void UpdateAuthor( int id,Author author);
    void DeleteAuthor(int id);
}