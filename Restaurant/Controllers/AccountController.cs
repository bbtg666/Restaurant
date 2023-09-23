using Business.Account;
using Core.Constants;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;

        public AccountController(IAccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromForm]LoginRequest request, 
            [FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            var isSuccess = await _accountBusiness.Login(request, returnUrl);

            if (!isSuccess)
            {
                TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.LOGIN_FAILED;
                return View(request);
            }


            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm]SignUpRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            var errors = await _accountBusiness.SignUp(request);

            if (errors is null)
            {
                TempData[Constants.MESSAGE.ERROR_KEY] = Constants.MESSAGE.SIGNUP_FAILED;
                return View(request);
            }

            if(errors.Count > 0)
            {
                var message = string.Join("<br />", errors.ToArray());
                TempData[Constants.MESSAGE.ERROR_KEY] = message;
                return View(request);
            }

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountBusiness.Logout();

            return Redirect("/");
        }
    }
}
