using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWorkRepository _unitofwork;

    public CoverTypeController(IUnitOfWorkRepository unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objectlist = _unitofwork.CoverType.GetAll();
        return View(objectlist);
    }

    //GET

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitofwork.CoverType.Add(obj);
            _unitofwork.Save();
            TempData["success"] = "CoverType Created Successfully";
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
        var categoryfromDb = _unitofwork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (categoryfromDb == null)
        {
            return NotFound();
        }
        return View(categoryfromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        
        if (ModelState.IsValid)
        {
            _unitofwork.CoverType.update(obj);
            _unitofwork.Save();
            TempData["success"] = "CoverType Edited Successfully";
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
        var categoryfromDb = _unitofwork.CoverType.GetFirstOrDefault(u => u.Id == id);

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
        var obj = _unitofwork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }
        _unitofwork.CoverType.Remove(obj);
        _unitofwork.Save();

        TempData["success"] = "CoverType Deleted Successfully";
        return RedirectToAction("Index");

    }


}
