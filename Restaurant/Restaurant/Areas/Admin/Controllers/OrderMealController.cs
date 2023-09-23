using Business.Restaurant;
using Core.Constants;
using Core.Helper;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area(Constants.AREA.ADMIN)]
    [Authorize(Roles = $"{Constants.ROLE.ADMIN}")]
    public class OrderMealController : Controller
    {
        private readonly IMealBusiness _mealBusiness;

        public OrderMealController(IMealBusiness mealBusiness)
        {
            _mealBusiness = mealBusiness;
        }
        public IActionResult Index(SearchModel searchModel)
        {
            var model = _mealBusiness.GetUserMealOrders(searchModel);
            SetStatusSelectBoxValue(searchModel.Status ?? -1);
            ViewBag.ContentSearch = searchModel.Content;
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeStatus([FromBody] ChangeOrderStatusRequest request)
        {
            var isSuccess = _mealBusiness.ChangeOrderMealStatus(request.ID, request.Status);
            return Json(new { isSuccess });
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
    }
}
