using gergergergerg.Data;
using gergergergerg.Models;
using gergergergerg.Models.ViewModels;
using gergergergerg.Repository.IRepository;
using gergergergerg.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace gergergergerg.Controllers
{
    //[Authorize(WC.AdminRole)]
    public class InquiryController : Controller
    {

        private IInquiryDetailsRepository _inqDRepo;
        private IInquiryHeaderRepository _inqHRepo;
        [BindProperty]
        public InquiryVM InquiryVM{get;set;}
        public InquiryController(IInquiryDetailsRepository inqDRepo, IInquiryHeaderRepository inqHRepo)
        {
            _inqDRepo = inqDRepo;
            _inqHRepo = inqHRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            InquiryVM = new InquiryVM()
            {
                InquiryHeader =  _inqHRepo.FirstOrDefault(u=>u.Id == id),
                InquiryDetail = _inqDRepo.GetAll(u=>u.InquiryHeaderId == id,includeProperties: "Product")
            };

            return View(InquiryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            InquiryVM.InquiryDetail = _inqDRepo.GetAll(u => u.InquiryHeaderId == InquiryVM.InquiryHeader.Id);
            foreach (var detail in InquiryVM.InquiryDetail)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {

                    ProductId = detail.ProductId,
                };
                shoppingCartList.Add(shoppingCart);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            HttpContext.Session.Set(WC.SessionInquiryId, InquiryVM.InquiryHeader.Id);



            return RedirectToAction("Index","Cart");
        }
        [HttpPost]
        public IActionResult Delete()
        {
            InquiryHeader inquiryHeader = _inqHRepo.FirstOrDefault(u=> u.Id == InquiryVM.InquiryHeader.Id);
            IEnumerable<InquiryDetails>inquiryDetails = _inqDRepo.GetAll(u => u.InquiryHeaderId == InquiryVM.InquiryHeader.Id);
            _inqDRepo.RemoveRange(inquiryDetails);
            _inqHRepo.Remove(inquiryHeader);
            _inqHRepo.Save();
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = _inqHRepo.GetAll()});

        }
        #endregion
    } 
}
