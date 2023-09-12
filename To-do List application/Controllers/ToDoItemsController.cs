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
        public IActionResult CreateItem([Bind("Title, Description, Priority, ToDoListID")] ToDoItem item)
        {
            ViewData["listId"] = item.ToDoListID;

            // Check if an item with the same title already exists
            ToDoItem temp = _context.Item.FirstOrDefault(i => i.Title == item.Title);

            if (temp != null)
            {
                // Item with the same title exists; add a validation error and return to the view
                ModelState.AddModelError(string.Empty, "An item with this title already exists.");
                return View(item);
            }

            // If there are no validation errors, save the new item
            if (ModelState.IsValid)
            {
                _context.Item.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Details", "ToDoLists", new { id = item.ToDoListID });
            }           
            // If ModelState is not valid, return to the view to display validation errors
            return View(item);
        }
    }
}
