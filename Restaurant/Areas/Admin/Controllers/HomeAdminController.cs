using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area(Constants.AREA.ADMIN)]
    [Authorize(Roles = $"{Constants.ROLE.ADMIN}")]
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
