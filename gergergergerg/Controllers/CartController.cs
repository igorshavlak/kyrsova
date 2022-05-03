using gergergergerg.Data;
using gergergergerg.Models;
using gergergergerg.Models.ViewModels;
using gergergergerg.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace gergergergerg.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodListTemp = _db.Products.Where(u => prodInCart.Contains(u.Id));
            IList<Product> prodList = new List<Product>();
            foreach (var cartObj in shoppingCartList)
            {
                Product prodTemp = prodListTemp.FirstOrDefault(u => u.Id == cartObj.ProductId);
                prodTemp.TempCount = cartObj.Count;
                prodList.Add(prodTemp);
            }
            return View(prodList);
        }
        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<Product> ProdList)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            foreach (Product prod in ProdList)
            {
                shoppingCartList.Add(new ShoppingCart { ProductId = prod.Id, Count = prod.TempCount });

            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Summary));
        }
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Products.Where(u => prodInCart.Contains(u.Id));
            ProductUserVM = new ProductUserVM()
            {
                //ShoppingCartList = shoppingCartList,
                IdentityUser = _db.IdentityUsers.FirstOrDefault(u => u.Id == claims.Value)
                //ProductList = prodList.ToList()
            };
            foreach (var obj in shoppingCartList)
            {
                Product product = _db.Products.FirstOrDefault(u => u.Id == obj.ProductId);
                product.TempCount = obj.Count;
                ProductUserVM.ProductList.Add(product);
            }
            return View(ProductUserVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost(ProductUserVM ProductUserVM)
        {

            //var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
            //+ "templates" + Path.DirectorySeparatorChar.ToString() + "fffffffffffda.html";
            //var subject = "New Inquiry";
            //string HtmlBody = "";
            //using (StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            //{
            //    HtmlBody = sr.ReadToEnd();
            //}
            //StringBuilder productListSB = new StringBuilder();
            //foreach (var product in productUserVM.ProductList)
            //{
            //    productListSB.Append($" - Name: {product.Name} <span style = 'font-size:14px;'> (ID: {product.Id})</span><br / >");
            //}
            //string messageBody = string.Format(HtmlBody,
            //    ProductUserVM.IdentityUser.UserName,
            //    ProductUserVM.IdentityUser.Email,
            //    ProductUserVM.IdentityUser.PhoneNumber,
            //    productListSB.ToString());

            //await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messageBody);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            InquiryHeader inquiryHeader = new InquiryHeader()
            {
                IdentityUserId = claim.Value,
                UserName = ProductUserVM.IdentityUser.UserName,
                Email = ProductUserVM.IdentityUser.Email,
                PhoneNumber = ProductUserVM.IdentityUser.PhoneNumber,
                InquiryDate = DateTime.Now
            };
            _db.InquiryHeaders.Add(inquiryHeader);
            _db.SaveChanges();
            foreach (var prod in ProductUserVM.ProductList)
            {
                InquiryDetails inquiryDetails = new InquiryDetails()
                {
                    InquiryHeaderId = inquiryHeader.Id,
                    ProductId = prod.Id
                };
                _db.InquiryDetail.Add(inquiryDetails);

            }
            _db.SaveChanges();


            return RedirectToAction(nameof(InquiryConfirmation));
        }
        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public IActionResult UpdateCart(IEnumerable<Product> ProdList)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            foreach(Product prod in ProdList)
            {
                shoppingCartList.Add(new ShoppingCart { ProductId = prod.Id,Count = prod.TempCount });

            }
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }

    }
}

