using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class StudentService: IStudentServices
{
    private readonly string connectionString = DataContextHelper.GetConnectionString();
    public bool AddStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @" 
            insert into students(firstname,lastname,studentcode,email)
            values(@firstname,@lastname,@studentcode,@email);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("firstname", student.FirstName);
            command.Parameters.AddWithValue("lastname", student.LastName);
            command.Parameters.AddWithValue("studentcode", student.StudentCode);
            command.Parameters.AddWithValue("email", student.Email);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Added successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Student Not Added");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Student> GetAllStudents()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select * from students;";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            List<Student> students = new();
            while (reader.Read())
            {
                var student = new Student()
                {
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    StudentCode = reader.GetString(reader.GetOrdinal("studentcode")),
                    Email = reader.GetString(reader.GetOrdinal("email")),
                };
                students.Add(student);
            }
            connection.Close();
            if (students.Any())
            {
                Console.WriteLine(students.Count + " - Student Founded!");
                return students;
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

    public bool UpdateStudent(int id, Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            SELECT * FROM students WHERE id = @id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id", id);
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Student Not Found");
            }
            const string query1=@"update students set 
            firstname = @firstname, lastname = @lastname, studentcode = @studentcode, email = @email where id = @id;";
            var command1 = new NpgsqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@id", id);
            command1.Parameters.AddWithValue("@firstname", student.FirstName);
            command1.Parameters.AddWithValue("@lastname", student.LastName);
            command1.Parameters.AddWithValue("@studentcode", student.StudentCode);
            command1.Parameters.AddWithValue("@email", student.Email);
            var result = command1.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Updated Successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Student Not Updated");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteStudent(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"delete from students where id = @id;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id", id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Deleted Successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Student Not Deleted");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}