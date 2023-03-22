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
	private readonly IWebHostEnvironment _HostEnvironment;

	public ProductController(IUnitOfWorkRepository unitOfWork, IWebHostEnvironment HostEnvironment)
	{
		_unitOfWork = unitOfWork;
		_HostEnvironment = HostEnvironment;
	}

	public IActionResult Index()
	{
		return View();
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

		
		if (id == null || id == 0)
		{
			//create product
			
			return View(ProductVM);
		}
		else
		{
			//update product
		}


		return View(ProductVM);
	}

	//POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Upsert(ProductVM obj, IFormFile? file)
	{ 

		if (ModelState.IsValid)
		{
			string wwwRootPath = _HostEnvironment.WebRootPath;
			if(file!=null)
			{
				string fileName = Guid.NewGuid().ToString();
				var uploads = Path.Combine(wwwRootPath, @"images\Products");
				var extension = Path.GetExtension(file.FileName);
				using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
				{
					file.CopyTo(fileStreams);
				}
				obj.product.ImageUrl = @"\images\products\" + fileName + extension;
			}
#pragma warning disable CS8604 // Possible null reference argument.
			_unitOfWork.Product.Add(obj.product);
#pragma warning restore CS8604 // Possible null reference argument.
			_unitOfWork.Save();
			TempData["success"] = "Product created successfully";
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
		TempData["success"] = "Product deleted successfully";
		return RedirectToAction("Index");

	}
	#region APICALLS
	[HttpGet]

	public IActionResult GetAll()
	{
		var productList = _unitOfWork.Product.GetAll();
		return Json(new { data = productList });
	}

	#endregion
}