using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EFLinqToSql.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinqToSqlController : ControllerBase
    {
        public class Input
        {
            public string Code { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Input input)
        {
            var service = new EFLinqToSql();
            return await service.GetSqlAsync(input.Code);
        }
    }
}
