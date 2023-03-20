using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
	private readonly IUnitOfWorkRepository _unitOfWork;

	public ProductController(IUnitOfWorkRepository unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public IActionResult Index()
	{
		IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
		return View(objCoverTypeList);
	}

	//GET
	public IActionResult Upsert(int? id)
	{
		ProductVM ProductVM = new()
		{
			product = new(),
			CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
			{
				Text= i.Name,
				Value = i.ID.ToString()
			}),
			CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
			{
				Text= i.Name,
				Value = i.Id.ToString()
			})
		};

		Product product = new();
		IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
			u => new SelectListItem
			{
				Text= u.Name,
				Value= u.ID.ToString()
			});
		IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
			u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
		if (id == null || id == 0)
		{
			//create product
			ViewBag.CategoryList = CategoryList;
			ViewBag.CoverTypeList = CoverTypeList;
			return View(product);
		}
		else
		{
			//update product
		}


		return View(product);
	}

	//POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Upsert(CoverType obj)
	{

		if (ModelState.IsValid)
		{
			_unitOfWork.CoverType.update(obj);
			_unitOfWork.Save();
			TempData["success"] = "CoverType updated successfully";
			return RedirectToAction("Index");
		}
		return View(obj);
	}

	public IActionResult Delete(int? id)
	{
		if (id == null || id == 0)
		{
			return NotFound();
		}
		var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

		if (CoverTypeFromDbFirst == null)
		{
			return NotFound();
		}

		return View(CoverTypeFromDbFirst);
	}

	//POST
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public IActionResult DeletePOST(int? id)
	{
		var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
		if (obj == null)
		{
			return NotFound();
		}

		_unitOfWork.CoverType.Remove(obj);
		_unitOfWork.Save();
		TempData["success"] = "CoverType deleted successfully";
		return RedirectToAction("Index");

	}
}