namespace lab_app.Middleware;

public class RequestStats
{
    private long _total;

    public long Total => Interlocked.Read(ref _total);

    public long Hit() => Interlocked.Increment(ref _total);
}