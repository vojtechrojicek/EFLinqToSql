using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFLinqToSql.Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiUrl = "http://localhost:5000/linqtosql";

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            EntitiesText = GetEntitiesText();
            DbContextText = GetDbContextText();
        }

        [BindProperty]
        public string ResponseText { get; set; }

        [BindProperty]
        public string EntitiesText { get; set; }

        [BindProperty]
        public string DbContextText { get; set; }

        public async Task OnPost()
        {
            ResponseText = string.Empty;
            string text = Request.Form["linq"];

            var code = new { code = text };
            var json = JsonSerializer.Serialize(code);

            var client = _clientFactory.CreateClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                ResponseText = await response.Content.ReadAsStringAsync();
            }
            else
            {
                ResponseText = "UPS...Error on server.";
            }
        }

        private string GetEntitiesText()
        {
            return @"public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImgUri { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}";
        }

        public string GetDbContextText()
        {
            return @"public class BaseDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    ...
}";
        }
    }
}
