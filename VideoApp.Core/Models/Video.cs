using SQLite;

namespace VideoApp.Core.Models
{
    public class Video
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }
        public string Thumbnail { get; set; }
        public DateTime DateAdded { get; set; }
    }

}