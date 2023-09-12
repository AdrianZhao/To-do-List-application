using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_do_List_application.Data;
using To_do_List_application.Models;
using To_do_List_application.ViewModels;

namespace To_do_List_application.Controllers
{
    public class ToDoListsController : Controller
    {
        private ToDoListApplicationDbContext _context;

        public ToDoListsController(ToDoListApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoLists
        [Route("/")]
        [Route("/lists")]
        public IActionResult Index()
        {
            if (_context.List == null)
            {
                return NotFound();
            }
            else
            {
                return View(_context.List.Include(s => s.Items).ToHashSet());
            }
        }
        // GET: ToDoLists/Create
        [Route("/lists/create")]
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [Route("/lists/create")]
        [HttpPost]
        public IActionResult Create([Bind("Title")] ToDoList list)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.List.Add(list);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = list.ToDoListID });
        }

        // GET: ToDoLists/Details/
        [Route("/lists/details")]
        public IActionResult Details(int? id)
        {
            if (id == null || _context.List == null)
            {
                return NotFound();
            }
            ToDoList? toDoList = _context.List.Include(list => list.Items).FirstOrDefault(m => m.ToDoListID == id);
            if (toDoList == null)
            {
                return NotFound();
            }
            List<ToDoItem> completedItems = new List<ToDoItem>();
            List<ToDoItem> notCompletedItems = new List<ToDoItem>();
            if (toDoList.Items != null)
            {
                if (toDoList.Items.Count(i => i.Completed) != 0)
                {
                    completedItems = toDoList.Items.Where(item => item.Completed).ToList();
                }
                if (toDoList.Items.Count(i => !i.Completed) != 0)
                {
                    notCompletedItems = toDoList.Items.Where(item => !item.Completed).ToList();
                }
            }
            ToDoListDetailsViewModel viewModel = new ToDoListDetailsViewModel
            {
                ToDoList = toDoList,
                CompletedItems = completedItems,
                NotCompletedItems = notCompletedItems
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("/lists/details")]
        public IActionResult ToggleCompleted(int? id)
        {
            if (id == null || _context.Item == null)
            {
                return NotFound();
            }
            ToDoItem? toDoItem = _context.Item.FirstOrDefault(i =>i.ToDoItemID == id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            toDoItem.Completed = !toDoItem.Completed;
            _context.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Details", new { id = toDoItem.ToDoListID });
        }

        [Route("/lists/delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ToDoList? toDoList = _context.List.Include(list => list.Items).FirstOrDefault(list => list.ToDoListID == id);
            if (toDoList == null)
            {
                return NotFound();
            }
            List<ToDoItem> completedItems = toDoList.Items.Where(item => item.Completed).ToList();
            List<ToDoItem> notCompletedItems = toDoList.Items.Where(item => !item.Completed).ToList();
            ToDoListDetailsViewModel viewModel = new ToDoListDetailsViewModel
            {
                ToDoList = toDoList,
                CompletedItems = completedItems,
                NotCompletedItems = notCompletedItems
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("/lists/delete")]
        public IActionResult ConfirmDelete(int id)
        {
            ToDoList? toDelete = _context.List.Find(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            else
            {
                _context.List.Remove(toDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
