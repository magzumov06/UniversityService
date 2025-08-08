namespace Infrastructure.Interfaces;

public interface IBorrowBookServices
{
    bool BorrowBook(int StudentID, int BookID,DateOnly BorrowDate);
    bool ReturnBook(int BorrowingID,DateOnly ReturnDate);
    bool GetBorrowingsByStudent(int studentId);
    bool DeleteBorrowing(int borrowingId);
}