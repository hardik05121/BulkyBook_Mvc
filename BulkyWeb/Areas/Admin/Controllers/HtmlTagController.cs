using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using System.Collections.Generic;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HtmlTagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HtmlTagController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<HtmlTag> objHtmlTagList = _unitOfWork.HtmlTag.GetAll().ToList();

            return View();
        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                //create
                return View(new HtmlTag());
            }
            else
            {
                //update
                HtmlTag htmlTagObj = _unitOfWork.HtmlTag.Get(u => u.Id == id);
                return View(htmlTagObj);
            }



        }
        [HttpPost]
        public IActionResult Upsert(HtmlTag htmlTag, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string htmltagPath = Path.Combine(wwwRootPath, @"images\htmltag");

                    if (!string.IsNullOrEmpty(htmlTag.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, htmlTag.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(htmltagPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    htmlTag.ImageUrl = @"\images\htmltag\" + fileName;
                }

                if (htmlTag.Id == 0)
                {
                    _unitOfWork.HtmlTag.Add(htmlTag);
                }
                else
                {
                    _unitOfWork.HtmlTag.Update(htmlTag);
                }

                _unitOfWork.Save();
                TempData["success"] = "HtmlTag created successfully";
                return RedirectToAction("Index");
            }
            else
            {
               
                return View(htmlTag);
            }
        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<HtmlTag> objHtmlTagList = _unitOfWork.HtmlTag.GetAll().ToList();

            return Json(new { data = objHtmlTagList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var htmlTagToBeDeleted = _unitOfWork.HtmlTag.Get(u => u.Id == id);
            if (htmlTagToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           htmlTagToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.HtmlTag.Remove(htmlTagToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}