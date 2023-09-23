using Microsoft.AspNetCore.Identity;
using Core.Constants;
using Core.Models.Requests;
using Data.Entities;
using Data.Services;

namespace Business.Account
{
    public interface IAccountBusiness
    {
        Task<bool> Login(LoginRequest request, string returnUrl);
        Task<List<string>> SignUp(SignUpRequest request);
        Task<bool> Logout();
    }
    public class AccountBusiness : IAccountBusiness
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        public AccountBusiness(SignInManager<IdentityUser> signInManager, 
                                UserManager<IdentityUser> userManager,
                                IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<bool> Login(LoginRequest request, string returnUrl)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return false;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    return false;
                }

                await _signInManager.SignInAsync(user, request.RememberMe);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<string>> SignUp(SignUpRequest request)
        {
            try
            {
                var identityUser = new IdentityUser()
                {
                    UserName = request.UserName,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(identityUser, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    return errors;
                }

                var accountId = await _userManager.GetUserIdAsync(identityUser);

                if (accountId is null)
                {
                    return null;
                }

                var user = new User()
                {
                    AccountId = accountId,
                    Address = request.Address,
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber
                };

                var isSuccess = await _userService.AddUser(user);

                if (!isSuccess)
                {
                    var logins = await _userManager.GetLoginsAsync(identityUser);

                    foreach (var login in logins)
                    {
                        await _userManager.RemoveLoginAsync(identityUser, login.LoginProvider, login.ProviderKey);
                    }

                    return null;
                }

                var role = await _userManager.AddToRoleAsync(identityUser, Constants.ROLE.MEMBER);
                await _signInManager.SignInAsync(identityUser, false);

                return new List<string>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}