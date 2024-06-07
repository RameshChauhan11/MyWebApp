using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.categories;
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                _context.categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category Added Done!";

                return RedirectToAction("Index");
            }
            return View(category);
            
        }
        public IActionResult Edit(int? id)
        {
            if(id==null&& id==0)
            {
                return NotFound();
            }
            var category = _context.categories.Find(id);
           
            if (category==null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {

                _context.categories.Update(category);
                TempData["success"] = "Category Updated Done!";
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);

        }
        public IActionResult delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            var category = _context.categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int? id)
        {
            if(id==null && id==0)
            {
                return NotFound();
            }
            var category = _context.categories.Find(id);
            if (category==null)
            {
                return NotFound();
            }
            _context.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Done!";

            return RedirectToAction("index");
            
        }
    }
}
