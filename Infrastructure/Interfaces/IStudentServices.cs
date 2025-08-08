using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IStudentServices
{
    bool AddStudent(Student student);
    List<Student> GetAllStudents();
    bool UpdateStudent(int id, Student student);
    bool DeleteStudent(int id);
}