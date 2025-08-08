namespace Domain.Models;

public class Book
{
    public int Id{get;set;}
    public string Title{get;set;}
    public int Category_id{get;set;}
    public int PublicationYear{get;set;}
    public string Isbn{get;set;}
}