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
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<City> objCityList = _unitOfWork.City.GetAll(includeProperties: "State").ToList();

            return View(objCityList);
        }

        public IActionResult Upsert(int? id)
        {
            CityVM cityVM = new()
            {
                StateList = _unitOfWork.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                City = new City()
            };
            if (id == null || id == 0)
            {
                //create
                return View(cityVM);
            }
            else
            {
                //update
                cityVM.City = _unitOfWork.City.Get(u => u.Id == id);
                return View(cityVM);

            }

            //else
            //{
            //    //update
            //    stateVM.City = _unitOfWork.City.Get(u => u.Id == id, includeProperties: "ProductImages");
            //    return View(stateVM);
            //}

        }
        [HttpPost]
        public IActionResult Upsert(CityVM cityVM)
        {
            if (ModelState.IsValid)
            {
                if (cityVM.City.Id == 0)
                {
                    _unitOfWork.City.Add(cityVM.City);
                }
                else
                {
                    _unitOfWork.City.Update(cityVM.City);
                }

                _unitOfWork.Save();
                TempData["success"] = "City created successfully";
                return RedirectToAction("Index");
            }


            else
            {
                cityVM.StateList = _unitOfWork.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(cityVM);
            }
        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<City> objCityList = _unitOfWork.City.GetAll(includeProperties: "State").ToList();
            return Json(new { data = objCityList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var cityToBeDeleted = _unitOfWork.City.Get(u => u.Id == id);
            if (cityToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }




            _unitOfWork.City.Remove(cityToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}