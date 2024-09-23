namespace Marvel_Application.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }
}