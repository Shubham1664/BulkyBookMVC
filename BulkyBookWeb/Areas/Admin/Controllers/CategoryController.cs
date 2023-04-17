using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWorkRepository _unitofwork;

    
    public CategoryController(IUnitOfWorkRepository unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objectlist = _unitofwork.Category.GetAll();
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
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "Order name and display order cannot be same");
        }
        if (ModelState.IsValid)
        {
            _unitofwork.Category.Add(obj);
            _unitofwork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);

    }

    //GET

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var categoryfromDb = _unitofwork.Category.GetFirstOrDefault(u => u.ID == id);

        if (categoryfromDb == null)
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
            _unitofwork.Category.Update(obj);
            _unitofwork.Save();
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
        var categoryfromDb = _unitofwork.Category.GetFirstOrDefault(u => u.ID == id);

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
        var obj = _unitofwork.Category.GetFirstOrDefault(u => u.ID == id);

        if (obj == null)
        {
            return NotFound();
        }
        _unitofwork.Category.Remove(obj);
        _unitofwork.Save();

        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");

    }


}
