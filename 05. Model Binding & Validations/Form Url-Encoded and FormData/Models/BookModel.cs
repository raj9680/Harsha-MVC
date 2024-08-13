namespace Form_Url_Encoded_and_FormData.Models
{
    public class BookModel
    {
        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book id: {BookId}, Author: {Author}";
        }
    }
}
