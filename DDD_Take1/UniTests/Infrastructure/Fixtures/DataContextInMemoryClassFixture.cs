using Microsoft.EntityFrameworkCore;

public class DataContextInMemoryClassFixture : IDisposable
{
    private DbContextOptions<DddContext>? dbContextOptions;
    private DddContext dddContext;

    public DddContext DddContext
    {
        get
        {
            if (dddContext is null)
            {
                dddContext = new DddContext(dbContextOptions!);
            }

            return dddContext;
        }
    }

    public DataContextInMemoryClassFixture()
    {
        Setup();
    }

    private void Setup()
    {
        var dbName = $"{DateTime.Now.ToFileTimeUtc()}";

        dbContextOptions = new DbContextOptionsBuilder<DddContext>()
            .UseInMemoryDatabase(dbName)
            .EnableSensitiveDataLogging()
            .Options;
    }

    private void Populate()
    {

    }

    public void Dispose()
    {
        dbContextOptions = null;
    }
}