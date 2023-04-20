using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWorkRepository _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWorkRepository unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

        return View(productList);
    }

    public IActionResult Details(int productId)
    {
        ShoppingCart cartObj = new()
        {
            Count = 1,
            ProductId = productId,
            Product = _unitOfWork.Product.GetFirstOrDefault(u => u.ID == productId, includeProperties: "Category,CoverType"),
        };

        return View(cartObj);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        shoppingCart.RegisteredUserId = claim.Value;

       ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => 
       u.RegisteredUserId == shoppingCart.RegisteredUserId && 
       u.ProductId == shoppingCart.ProductId, includeProperties: "Product");


        if (cartFromDb == null)
        {

            _unitOfWork.ShoppingCart.Add(shoppingCart);
        }
        else
        {
            _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
        }
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}