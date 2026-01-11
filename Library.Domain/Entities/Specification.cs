using Library.Shared.Extensions;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities
{
    public class Specification
    {
        [JsonPropertyName("Originally published")]
        public string? OriginallyPublished { get; set; }
        [JsonPropertyName("Author")]
        public string? Author { get; set; }
        [JsonPropertyName("Page count")]
        public int PageCount { get; set; }
        [JsonPropertyName("Illustrator")]
        [JsonConverter(typeof(StringOrArrayConverter))]
        public List<string> Illustrator { get; set; } = [];
        [JsonPropertyName("Genres")]
        [JsonConverter(typeof(StringOrArrayConverter))]
        public List<string> Genres { get; set; } = [];

        public Specification()
        {
            
        }
    }
}
