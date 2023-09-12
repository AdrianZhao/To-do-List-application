using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_do_List_application.Models
{
    public class ToDoItem
    {
        [Key]
        public int ToDoItemID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "Title must be less than 250 characters.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Creation")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Priority is required.")]
        public Priority Priority { get; set; }

        [MaxLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
        public string? Description { get; set; }

        public bool Completed { get; set; } = false;
        public int ToDoListID { get; set; }
        public ToDoList? List { get; set; }
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }
}
