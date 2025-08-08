using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class AuthorService : IAuthorServices
{
    private readonly string connectionString = DataContextHelper.GetConnectionString();
    public void AddAuthor(Author author)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query =
                @"insert into authors(firstname,lastname,country) values(@firstname,@lastname,@country);";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", author.FirstName);
            command.Parameters.AddWithValue("@lastname", author.LastName);
            command.Parameters.AddWithValue("@country", author.Country);
            var effect =  command.ExecuteNonQuery();
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Author added successfully");
            }
            else
            {
                Console.WriteLine("Author could not be added");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Author> GetAllAuthors()
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        const string query = @"select * from authors;";
        using var command = new NpgsqlCommand(query, connection);
        var reader = command.ExecuteReader();
        List<Author> authors = new();
        while (reader.Read())
        {
            var author = new Author()
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                LastName = reader.GetString(reader.GetOrdinal("lastname")),
                Country = reader.GetString(reader.GetOrdinal("country")),
            };
            authors.Add(author);
        }
        connection.Close();
        if (authors.Any())
        {
            Console.WriteLine(authors.Count + " - Author Founded!");
            return authors;
        }
        else
        {
            throw new Exception("No Authors found");
        }
    }

    public void UpdateAuthor(int id, Author author)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"SELECT * FROM authors WHERE id = @id;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Author not found");
            }
            const string query2 = @"
            update authors set firstname = @firstname, lastname = @lastname ,country = @country where id = @id;
           ";
            using var command2 = new NpgsqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@id", id);
            command2.Parameters.AddWithValue("@firstname", author.FirstName);
            command2.Parameters.AddWithValue("@lastname", author.LastName);
            command2.Parameters.AddWithValue("@country", author.Country);
            var effect = command2.ExecuteNonQuery();
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Author updated successfully");
            }
            else
            {
                Console.WriteLine("Author could not be updated");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void DeleteAuthor(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"DELETE FROM authors WHERE id = @id;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Author deleted successfully");
            }
            else
            {
                throw new Exception("Author could not be deleted");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
