using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class BookServices:IBookServices
{
    private readonly string connectionString = DataContextHelper.GetConnectionString();
    public bool AddBook(Book book)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            insert into book(title, Category_id, publicationyear,isbn)
            values(@title, @Category_id, @PublicationYear, @Isbn);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@Category_id", book.Category_id);
            command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            command.Parameters.AddWithValue("@Isbn", book.Isbn);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Book Added successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Book Not Added");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Book> GetAllBooks()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"SELECT * FROM books;";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            List<Book> books = new();
            if (reader.Read())
            {
                var book = new Book()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Title = reader.GetString(reader.GetOrdinal("title")),
                    Category_id = reader.GetInt32(reader.GetOrdinal("category_id")),
                    PublicationYear = reader.GetInt32(reader.GetOrdinal("publicationyear")),
                    Isbn = reader.GetString(reader.GetOrdinal("isbn"))
                };
                books.Add(book);
            }
            connection.Close();
            if (books.Any())
            {
                Console.WriteLine(books.Count + " - Books Found!");
                return books;
            }
            else
            {
                throw new Exception("No Books Found");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool UpdateBook(int id, Book book)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query=@"
            select *  from books where id = @id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if(!)
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteBook(int id)
    {
        throw new NotImplementedException();
    }
}