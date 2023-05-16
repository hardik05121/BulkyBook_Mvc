using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using System.Collections.Generic;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ContactUsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ContactUsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }
        public IActionResult Index()
        {
            //List<ContactUs> objContactUsList = _unitOfWork.ContactUs.GetAll(includeProperties: "Country,State,City").ToList();

            return View();
        }
        public JsonResult GetCountries()
        {
            var countries = _unitOfWork.Country.GetAll();
            return new JsonResult(countries);
        }
        public JsonResult GetStates(int Countryid)
        {
            var states = _unitOfWork.State.GetAll(u => u.Country.Id == Countryid).ToList();
            return new JsonResult(states);
        }
        public JsonResult GetCities(int Stateid)
        {
            var cities = _unitOfWork.City.GetAll(u => u.State.Id == Stateid).ToList();
            return new JsonResult(cities);
        }

        public IActionResult Upsert(int? id)
        {
            ContactUsVM contactUsVM = new()
            {
                CountryList = _unitOfWork.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                StateList = _unitOfWork.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CityList = _unitOfWork.City.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                ContactUs = new ContactUs()
            };
            if (id == null || id == 0)
            {
                //create
                return View(contactUsVM);
            }
            else
            {
                //update
                contactUsVM.ContactUs = _unitOfWork.ContactUs.Get(u => u.Id == id);
                return View(contactUsVM);
              
            }

            //else
            //{
            //    //update
            //    contactUsVM.ContactUs = _unitOfWork.ContactUs.Get(u => u.Id == id, includeProperties: "ProductImages");
            //    return View(contactUsVM);
            //}

        }
        [HttpPost]
        public IActionResult Upsert(ContactUsVM contactUsVM)
        {
            if (ModelState.IsValid)
            {
                if (contactUsVM.ContactUs.Id == 0)
                {
                    _unitOfWork.ContactUs.Add(contactUsVM.ContactUs);
                }
                else
                {
                    _unitOfWork.ContactUs.Update(contactUsVM.ContactUs);
                }

                _unitOfWork.Save();
                TempData["success"] = "ContactUs created successfully";
                return RedirectToAction("Index");
                }


            else
            {
                contactUsVM.CountryList = _unitOfWork.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                contactUsVM.StateList = _unitOfWork.State.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                contactUsVM.CityList = _unitOfWork.City.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(contactUsVM);
            }
        }

     

        #region API CALL

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ContactUs> objContactUsList = _unitOfWork.ContactUs.GetAll(includeProperties: "Country,State,City").ToList();
            return Json(new { data = objContactUsList });
        }
        



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var contactUsToBeDeleted = _unitOfWork.ContactUs.Get(u => u.Id == id);
            if (contactUsToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

           


            _unitOfWork.ContactUs.Remove(contactUsToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}