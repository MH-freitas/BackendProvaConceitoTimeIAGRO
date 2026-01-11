using Library.Domain.Entities;
using System.Text.Json;

namespace Library.Infra.AccessConfiguration
{
    public static class DataAccessConfiguration
    {
        public static async Task<IReadOnlyList<Book>?> GetBooksAsync()
        {
            var json = await File.ReadAllTextAsync("../Library.Infra/Data/Books.json");

            var booksList = JsonSerializer.Deserialize<IReadOnlyList<Book>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return booksList;
        }
    }

}