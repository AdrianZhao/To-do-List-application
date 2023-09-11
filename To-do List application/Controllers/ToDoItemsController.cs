using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_do_List_application.Data;
using To_do_List_application.Models;

namespace To_do_List_application.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ToDoListApplicationDbContext _context;

        public ToDoItemsController(ToDoListApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/lists/add-item")]
        public IActionResult CreateItem(int? listId)
        {
            if (listId == null)
            {
                return NotFound();
            }           
            ViewData["listId"] = listId;
            return View();
        }

        [HttpPost]
        [Route("/lists/add-item")]
        public IActionResult CreateItem([Bind("Title, Description, Priority")] ToDoItem item)
        {
            if (TryValidateModel(item))
            {
                _context.Item.Add(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Lists");
        }
    }
}
