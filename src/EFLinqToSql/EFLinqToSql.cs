using EFLinqToSql.Data;
using EFLinqToSql.Logging;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;
using System.Threading.Tasks;

namespace EFLinqToSql
{
    public class EFLinqToSql
    {
        public async Task<string> GetSqlAsync(string linq)
        {
            var options = ScriptOptions.Default
                .AddReferences(typeof(Product).Assembly)
                .AddImports("System.Linq", "EFLinqToSql.Data");

            BaseDbContext dbContext = BaseDbContextLoggedFactory.Create(out CustomLogMessage message);
            IQueryable<Product> iQueryable = await CSharpScript.EvaluateAsync<IQueryable<Product>>(linq, options, globals: dbContext);

            try
            {
                _ = iQueryable.ToList();
            }
            catch
            {
                //Swallow exception because dbContext do not exist properly.
            }

            return message.Information;
        }
    }
}
