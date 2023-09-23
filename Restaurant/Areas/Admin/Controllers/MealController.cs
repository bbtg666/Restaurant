using Business.Restaurant;
using Core.Constants;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area(Constants.AREA.ADMIN)]
    [Authorize(Roles = $"{Constants.ROLE.ADMIN}")]
    public class MealController : Controller
    {
        private readonly IMealBusiness _mealBusiness;

        public MealController(IMealBusiness mealBusiness)
        {
            _mealBusiness = mealBusiness;
        }

        public IActionResult Index(SearchModel searchModel)
        {
            var model = _mealBusiness.GetMeals(searchModel);
            SetSearchInput(searchModel);
            return View(model);
        }

        public IActionResult Create()
        {
            SetCategorySelectBoxValue();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MealRequest request)
        {
            if (ModelState.IsValid)
            {
                var newMeal = await _mealBusiness.AddMeal(request);

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

            SetCategorySelectBoxValue();
            return View(request);
        }

        public IActionResult Edit([FromRoute] int id)
        {
            var meal = _mealBusiness.GetMealById(id);
            SetCategorySelectBoxValue();
            return View(meal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] MealRequest request)
        {
            if (ModelState.IsValid)
            {
                var newTask = await _mealBusiness.UpdateMeal(request);

                if (newTask is not null)
                {
                    TempData[Constants.MESSAGE.SUCCESS_KEY] = Constants.MESSAGE.UPDATE_ITEM_SUCCESS;
                }
                else
                {
                    TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.UPDATE_ITEM_FAILED;
                }

                return RedirectToAction("Index");
            }

            SetCategorySelectBoxValue();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var isSuccess = await _mealBusiness.DeleteMeal(id);

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

        [NonAction]
        private void SetSearchInput(SearchModel searchModel)
        {
            ViewBag.ContentSearch = searchModel.Content ?? string.Empty;
            SetCategorySelectBoxValue(searchModel.Category ?? -1);
        }

        [NonAction]
        private void SetCategorySelectBoxValue(int selectedId = -1)
        {

            var selectBoxValue = new List<SelectListItem>();
            var categories = _mealBusiness.GetMealCategorys();

            foreach (var category in categories)
            {
                selectBoxValue.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.ID.ToString(),
                    Selected = selectedId == category.ID ? true : false
                });
            }

            ViewBag.CategorySelectBoxValue = selectBoxValue;
        }
    }
}
