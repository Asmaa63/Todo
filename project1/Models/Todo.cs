namespace project1.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdate { get; set; }
        public bool isDone { get; set; }
        public int? categoryid { get; set; }
        public Category? category { get; set; }
    }
}
