using gergergergerg.Data;
using gergergergerg.Models;
using gergergergerg.Models.ViewModels;
using gergergergerg.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace gergergergerg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;

        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Products.Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _db.Categories,
                ApplicationTypes = _db.ApplicationType,
                
            };

            return View(homeVM);
        }
        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            DetailsVM detailsVM = new DetailsVM()
            {

                Product = _db.Products.Include(u => u.Category).Include(u => u.ApplicationType)
                .Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false    
            };
            foreach(var item in shoppingCartsList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                }
                    
            }
            return View(detailsVM);


        }
        [HttpPost,ActionName("Details")]        
        public IActionResult DetailsPost(int id,DetailsVM detailsVM)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            shoppingCartsList.Add(new ShoppingCart() { ProductId = id, Count = detailsVM.Product.TempCount});
            HttpContext.Session.Set(WC.SessionCart, shoppingCartsList);
            return RedirectToAction(nameof(Index));
           
        }
        
        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            var itemToRemove = shoppingCartsList.SingleOrDefault(r => r.ProductId == id);
            if(itemToRemove != null)
            {
                shoppingCartsList.Remove(itemToRemove);
            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartsList);
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
}
