using System.ComponentModel.DataAnnotations;

namespace To_do_List_application.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoListID { get; set; }
        [MaxLength(25, ErrorMessage = "Title must less than 25 characters.")]
        [Display(Name = "Title")]
        public string? Title { get; set; }
        public List<ToDoItem> Items { get; set; }
    }
}
