using EFLinqToSql.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFLinqToSql.Data
{
    internal class BaseDbContextLoggedFactory
    {
        public static BaseDbContext Create(out CustomLogMessage message)
        {
            var customMessage = new CustomLogMessage();
            var MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddProvider(new CustomLoggerProvider(customMessage)); });

            var builder = new DbContextOptionsBuilder<BaseDbContext>();
            builder.UseLoggerFactory(MyLoggerFactory);
            builder.EnableSensitiveDataLogging();
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");

            message = customMessage;
            return new BaseDbContext(builder.Options);
        }
    }
}
