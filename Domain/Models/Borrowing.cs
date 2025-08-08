namespace Domain.Models;

public class Borrowing
{
    public int Id{get;set;}
    public int StudentId{get;set;}
    public int BookId{get;set;}
    public DateOnly BorrowDate{get;set;}
    public DateOnly ReturnDate{get;set;}
}