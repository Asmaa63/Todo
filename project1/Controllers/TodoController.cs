using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project1.Context;
using project1.Models;

namespace project1.Controllers
{
	[Authorize]
    public class TodoController : Controller
    {
        TodoContext context = new TodoContext();
		[AllowAnonymous]
        public IActionResult Index()
        {
            var todo = context.todos.Include(c=>c.category).ToList();
            return View(todo);
        }
        public IActionResult NewTodo()
        {
            ViewData["cate"] = context.categories.ToList();
            return View();
        }
		public IActionResult SaveNewTodo(Todo todo)
		{
			if (ModelState.IsValid)
			{
				todo.createdate = DateTime.Now;
				context.todos.Add(todo);
				context.SaveChanges();
			}
			ViewData["cate"] = context.categories.ToList();
			return View("NewTodo");
		}
		public IActionResult EditTodo(int id)
		{
			var todo = context.todos.FirstOrDefault(c => c.id == id);
			if (todo is null)
			{
				return View("Invalid");
			}
			ViewData["cate"] = context.categories.ToList();
			return View("NewTodo", todo);
		}
		public IActionResult SaveEditTodo(int id, Todo mtodo)
		{
			var todo = context.todos.FirstOrDefault(c => c.id == id);
			if (todo is null)
			{
				return View("Invalid");
			}
			if (ModelState.IsValid)
			{
				todo.name = mtodo.name;
				todo.description = mtodo.description;
				todo.categoryid = mtodo.categoryid;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewData["cate"] = context.categories.ToList();
			return View("NewTodo", mtodo);
		}
		public IActionResult RemoveTodo(int id)
		{
			var todo = context.todos.FirstOrDefault(c => c.id == id);
			if (todo is null)
			{
				return View("Invalid");
			}
			context.Remove(todo);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Toggle(int id)
		{
			var todo = context.todos.FirstOrDefault(a => a.id == id);
			if(todo == null)
			{
				return View("Invalid");
			}
			todo.isDone = !todo.isDone;
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
