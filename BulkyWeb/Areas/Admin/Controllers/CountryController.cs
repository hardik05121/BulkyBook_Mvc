using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Country> objCountryList = _unitOfWork.Country.GetAll().ToList();

            return View(objCountryList);
        }




        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                //create
                return View(new Country());
            }
            else
            {
                //update
                Country countryObj = _unitOfWork.Country.Get(u => u.Id == id);
                return View(countryObj);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Country CountryObj)
        {
            if (ModelState.IsValid)
            {

                if (CountryObj.Id == 0)
                {
                    _unitOfWork.Country.Add(CountryObj);
                }
                else
                {
                    _unitOfWork.Country.Update(CountryObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Country created successfully";
                return RedirectToAction("Index");
            }
            else
            {

                return View(CountryObj);
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Country> objCountryList = _unitOfWork.Country.GetAll().ToList();
            return Json(new { data = objCountryList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CountryToBeDeleted = _unitOfWork.Country.Get(u => u.Id == id);
            if (CountryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Country.Remove(CountryToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
