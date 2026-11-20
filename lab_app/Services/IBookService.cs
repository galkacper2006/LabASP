using lab_app.Models;

namespace lab_app.Services;

public interface IBookService
{
    Task<PagedResult<Book>> GetPageAsync(
        int page,
        int pageSize,
        string? search = null,
        string sort = "title",
        string dir = "asc");

    Task<Book?> GetByIdAsync(int id);

    Task<Book> AddAsync(Book book);

    Task<bool> UpdateAsync(Book book);

    Task<bool> DeleteAsync(int id);

    Task<bool> IsbnExistsAsync(
        string isbn,
        int? excludeId = null);

    Task<Book?> GetByIsbnAsync(string isbn);

    Task<IReadOnlyList<Book>> GetByYearAsync(int year);

    Task<IReadOnlyList<Book>> GetArchiveAsync(
        int year,
        int month);
}