using BulkyBook.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objectlist = _db.Categories;
            return View(objectlist);
        }

        //GET

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Order name and display order cannot be same");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
               
        }

        //GET

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.FirstOrDefault(u => u.ID == id);

            if(categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Order name and display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.FirstOrDefault(u => u.ID == id);

            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.ID == id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }


    }
}
