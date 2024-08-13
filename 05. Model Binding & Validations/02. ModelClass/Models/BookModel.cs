using Microsoft.AspNetCore.Mvc;

namespace ModelClass.Models
{
    public class BookModel
    {
        [FromQuery]
        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book Object - Book id {BookId}, Author: {Author}";
        }
    }
}
