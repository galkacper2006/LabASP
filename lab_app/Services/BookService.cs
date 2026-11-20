
using lab_app.Data;
using lab_app.Models;
using Microsoft.EntityFrameworkCore;

namespace lab_app.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _db;

    public BookService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<Book>> GetPageAsync(
        int page,
        int pageSize,
        string? search = null,
        string sort = "title",
        string dir = "asc")
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        IQueryable<Book> q = _db.Books.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            q = q.Where(b =>
                b.Title.Contains(search) ||
                b.Isbn.Contains(search));
        }

        var total = await q.CountAsync();

        var totalPages = (int)Math.Ceiling(
            total / (double)pageSize);

        page = Math.Min(page, Math.Max(1, totalPages));

        IOrderedQueryable<Book> ordered =
            (sort.ToLowerInvariant(), dir.ToLowerInvariant()) switch
            {
                ("price", "desc") =>
                    q.OrderByDescending(b => (double)b.Price),

                ("price", _) =>
                    q.OrderBy(b => (double)b.Price),

                ("date", "desc") =>
                    q.OrderByDescending(b => b.PublicationDate),

                ("date", _) =>
                    q.OrderBy(b => b.PublicationDate),

                ("title", "desc") =>
                    q.OrderByDescending(b => b.Title),

                _ =>
                    q.OrderBy(b => b.Title)
            };

        var items = await ordered
            .ThenBy(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Book>(
            items, page, pageSize, total);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _db.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> AddAsync(Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return book;
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        var existing = await _db.Books.FindAsync(book.Id);

        if (existing == null)
            return false;

        _db.Entry(existing).CurrentValues.SetValues(book);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _db.Books.FindAsync(id);

        if (book == null)
            return false;

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> IsbnExistsAsync(
        string isbn,
        int? excludeId = null)
    {
        return await _db.Books.AnyAsync(b =>
            b.Isbn == isbn &&
            (!excludeId.HasValue || b.Id != excludeId.Value));
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _db.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b =>
                b.Isbn.Replace("-", "") == isbn);
    }

    public async Task<IReadOnlyList<Book>> GetByYearAsync(int year)
    {
        return await _db.Books
            .AsNoTracking()
            .Where(b =>
                b.PublicationDate.HasValue &&
                b.PublicationDate.Value.Year == year)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Book>> GetArchiveAsync(
        int year, int month)
    {
        return await _db.Books
            .AsNoTracking()
            .Where(b =>
                b.PublicationDate.HasValue &&
                b.PublicationDate.Value.Year == year &&
                b.PublicationDate.Value.Month == month)
            .ToListAsync();
    }
}
