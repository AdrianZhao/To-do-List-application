using To_do_List_application.Models;
namespace To_do_List_application.ViewModels
{
    public class ToDoListDetailsViewModel
    {
        public ToDoList ToDoList { get; set; }
        public List<ToDoItem> CompletedItems { get; set; }
        public List<ToDoItem> NotCompletedItems { get; set; }
    }
}
