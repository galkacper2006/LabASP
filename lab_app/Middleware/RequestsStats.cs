using System.Collections.Concurrent;

namespace lab_app.Middleware;

public class RequestStats
{
    private long _total;

    private readonly ConcurrentDictionary<string, long> _byPath =
        new(StringComparer.OrdinalIgnoreCase);

    public long Total => Interlocked.Read(ref _total);

    public long Hit(string path)
    {
        var n = Interlocked.Increment(ref _total);

        _byPath.AddOrUpdate(path, 1, (_, old) => old + 1);

        return n;
    }

    public IReadOnlyList<KeyValuePair<string, long>> TopPaths(int count)
    {
        return _byPath
            .OrderByDescending(x => x.Value)
            .Take(count)
            .ToList();
    }
}