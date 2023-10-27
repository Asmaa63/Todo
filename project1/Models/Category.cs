using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Category
    {
        public int id { get; set; }
        [MinLength(3,ErrorMessage ="Min Length Should Be 3 Letters")]
        [MaxLength(20, ErrorMessage ="Max Length Should Be 20 Letters")]
        public string name { get; set; }
        public ICollection<Todo>? todos { get; set; }

    }
}
