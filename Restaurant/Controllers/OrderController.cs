using Business.Restaurant;
using Core.Constants;
using Core.Helper;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Controllers
{
    [Authorize(Roles = $"{Constants.ROLE.ADMIN},{Constants.ROLE.MEMBER}")]
    public class OrderController : Controller
    {
        private readonly IMealBusiness _mealBusiness;


        public OrderController(IMealBusiness mealBusiness)
        {
            _mealBusiness = mealBusiness;
        }

        public IActionResult OrderMeal(SearchModel searchModel)
        {
            var model = _mealBusiness.GetMeals(searchModel);
            SetCategorySelectBoxValue(searchModel.Category ?? -1);
            ViewBag.ContentSearch = searchModel.Content;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrderMeal()
        {
            await _mealBusiness.OrderMeal();
            return RedirectToAction("OrderMeal");
        }

        public IActionResult MyOrderMeal(SearchModel searchModel)
        {
            var model = _mealBusiness.GetUserMealOrdersByUser(searchModel);
            SetStatusSelectBoxValue(searchModel.Status ?? -1);
            ViewBag.ContentSearch = searchModel.Content;
            return View(model);
        }

        [HttpPost]
        public IActionResult Detail([FromBody] DetailOrderMealRequest request)
        {
            var model = _mealBusiness.GetUserOrderMealByID(request.ID);
            return View(model);
        }

        [NonAction]
        private void SetStatusSelectBoxValue(int status)
        {
            var statusSelectBoxValue = new List<SelectListItem>();

            foreach (var item in Enum.GetValues(typeof(Enums.OrderMealStatus)).Cast<Enums.OrderMealStatus>().ToList())
            {
                statusSelectBoxValue.Add(new SelectListItem
                {
                    Text = MiscHelper.GetDescription(item),
                    Value = ((int)item).ToString(),
                    Selected = status == (int)item
                });
            }

            ViewBag.StatusSelectBoxValue = statusSelectBoxValue;
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