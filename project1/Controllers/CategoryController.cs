using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project1.Context;
using project1.Models;

namespace project1.Controllers
{
    [Authorize(Policy ="Admin")]
    public class CategoryController : Controller
    {
        TodoContext context = new TodoContext();
        public IActionResult Index()
        {
            var context = new TodoContext();
            var category = context.categories.ToList();
            return View(category);
        }
        public IActionResult NewCategory()
        {
            return View();
        }
        public IActionResult SaveNewCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                context.categories.Add(category);
                context.SaveChanges();
            }
            return View("NewCategory");
        }
        public IActionResult EditCategory(int id)
        {
            var category = context.categories.FirstOrDefault(c => c.id == id);
            if(category is null)
            {
                return View("Invalid");
            }
            return View("NewCategory", category);
        }
        public IActionResult SaveEditCategory(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                var EditCategory = context.categories.FirstOrDefault(a => a.id == id);
                EditCategory.name = category.name;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewCategory", category);
        }
        public IActionResult RemoveCategory(int id)
        {
            var cate = context.categories.FirstOrDefault(c => c.id== id);
            if (cate is null)
            {
                return View("Invalid");
            }
            context.Remove(cate);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
