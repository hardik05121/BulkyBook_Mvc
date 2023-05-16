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
    public class StateController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public StateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<State> objStateList = _unitOfWork.State.GetAll(includeProperties: "Country").ToList();

            return View(objStateList);
        }

        public IActionResult Upsert(int? id)
        {
            StateVM stateVM = new()
            {
                CountryList = _unitOfWork.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                State = new State()
            };
            if (id == null || id == 0)
            {
                //create
                return View(stateVM);
            }
            else
            {
                //update
                stateVM.State = _unitOfWork.State.Get(u => u.Id == id);
                return View(stateVM);
              
            }

            //else
            //{
            //    //update
            //    stateVM.State = _unitOfWork.State.Get(u => u.Id == id, includeProperties: "ProductImages");
            //    return View(stateVM);
            //}

        }
        [HttpPost]
        public IActionResult Upsert(StateVM stateVM)
        {
            if (ModelState.IsValid)
            {
                if (stateVM.State.Id == 0)
                {
                    _unitOfWork.State.Add(stateVM.State);
                }
                else
                {
                    _unitOfWork.State.Update(stateVM.State);
                }

                _unitOfWork.Save();
                TempData["success"] = "State created successfully";
                return RedirectToAction("Index");
                }


            else
            {
                stateVM.CountryList = _unitOfWork.Country.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(stateVM);
            }
        }


       

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<State> objStateList = _unitOfWork.State.GetAll(includeProperties: "Country").ToList();
            return Json(new { data = objStateList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var stateToBeDeleted = _unitOfWork.State.Get(u => u.Id == id);
            if (stateToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

           


            _unitOfWork.State.Remove(stateToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}