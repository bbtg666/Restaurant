using Business.Restaurant;
using Core.Constants;
using Core.Models.Requests;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area(Constants.AREA.ADMIN)]
    [Authorize(Roles = $"{Constants.ROLE.ADMIN}")]
    public class MealCategoryController : Controller
    {
        private readonly IMealBusiness _mealBusiness;

        public MealCategoryController(IMealBusiness mealBusiness)
        {
            _mealBusiness = mealBusiness;
        }

        public IActionResult Index(SearchModel searchModel)
        {
            var model = _mealBusiness.GetMealCategorys();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MealCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var newMeal = await _mealBusiness.AddMealCategory(request);

                if (newMeal is not null)
                {
                    TempData[Constants.MESSAGE.SUCCESS_KEY] = Constants.MESSAGE.ADD_ITEM_SUCCESS;
                }
                else
                {
                    TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.ADD_ITEM_FAILED;
                }

                return RedirectToAction("Index");
            }

            return View(request);
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var meal = _mealBusiness.GetMealById(id);
            return View(meal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] MealCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var mealCategory = await _mealBusiness.UpdateMealCategory(request);

                if (mealCategory is not null)
                {
                    TempData[Constants.MESSAGE.SUCCESS_KEY] = Constants.MESSAGE.UPDATE_ITEM_SUCCESS;
                }
                else
                {
                    TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.UPDATE_ITEM_FAILED;
                }

                return RedirectToAction("Index");
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var isSuccess = await _mealBusiness.DeleteMealCategory(id);

            if (isSuccess)
            {
                TempData[Constants.MESSAGE.SUCCESS_KEY] = Constants.MESSAGE.DELETE_ITEM_SUCCESS;
            }
            else
            {
                TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.DELETE_ITEM_FAILED;
            }

            return RedirectToAction("Index");
        }

        //[NonAction]
        //private void SetStatusSelectBoxValue(int? status = -1)
        //{
        //    if (status is null)
        //    {
        //        status = -1;
        //    }

        //    var statusSelectBoxValue = new List<SelectListItem>();

        //    foreach (var item in Enum.GetValues(typeof(Enums.Status)).Cast<Enums.Status>().ToList())
        //    {
        //        statusSelectBoxValue.Add(new SelectListItem
        //        {
        //            Text = Enums.GetDescription(item),
        //            Value = ((int)item).ToString(),
        //            Selected = status == (int)item ? true : false
        //        });
        //    }

        //    ViewBag.StatusSelectBoxValue = statusSelectBoxValue;
        //}

        //[NonAction]
        //private void SetSearchInput(SearchModel searchModel)
        //{
        //    ViewBag.ContentSearch = searchModel.Content ?? string.Empty;
        //    SetStatusSelectBoxValue(searchModel.Status);
        //}

        //[NonAction]
        //private void SetAssgneeSelectBoxValue(string assignee)
        //{

        //    var selectBoxValue = new List<SelectListItem>();

        //    var users = _userManager.Users.ToList();

        //    foreach (var user in users)
        //    {
        //        selectBoxValue.Add(new SelectListItem
        //        {
        //            Text = user.UserName,
        //            Value = user.Id,
        //            Selected = assignee == user.Id ? true : false
        //        });
        //    }

        //    ViewBag.AssigneeSelectBoxValue = selectBoxValue;
        //}
    }
}
