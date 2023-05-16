using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Htag> objHtagList = _unitOfWork.Htag.GetAll().ToList();
            return View(objHtagList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TagVM obj)
        {
           
            Htag h = new Htag();
            h.FName = obj.Htag.FName;
            _unitOfWork.Htag.Add(h);
            _unitOfWork.Save();

            Itag i = new Itag();
            i.LName = obj.Itag.LName;
            _unitOfWork.Itag.Add(i);
            _unitOfWork.Save();
            TempData["success"] = "Tag created successfully";
            return RedirectToAction("Index");

           

        }
    }
}
