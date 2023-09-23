using AutoMapper;
using Core.Constants;
using Core.Helper;
using Core.Models.Requests;
using Data.Entities;
using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Business.Restaurant
{
    public interface IMealBusiness
    {
        // Meal
        List<Meal> GetMeals(SearchModel searchModel);
        Meal GetMealById(int id);
        Task<Meal> AddMeal(MealRequest request);
        Task<Meal> UpdateMeal(MealRequest request);
        Task<bool> DeleteMeal(int id);

        // Meal Category
        List<MealCategory> GetMealCategorys();
        MealCategory GetMealCategoryById(int id);
        Task<MealCategory> AddMealCategory(MealCategoryRequest request);
        Task<MealCategory> UpdateMealCategory(MealCategoryRequest request);
        Task<bool> DeleteMealCategory(int id);

        // Order
        Task<bool> OrderMeal();
        List<UserMealOrder> GetUserMealOrders(SearchModel searchModel);
        List<UserMealOrder> GetUserMealOrdersByUser(SearchModel searchModel);
        Task<bool> ChangeOrderMealStatus(int id, int status);
        UserMealOrder GetUserOrderMealByID(int id);
    }
    public class MealBusiness : IMealBusiness
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        private readonly ICartBusiness _cartBusiness;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public MealBusiness(IMealService mealService,
                            IMapper mapper,
                            ICartBusiness cartBusiness,
                            IOrderService orderService,
                            IUserService userService,
                            IHttpContextAccessor httpContextAccessor,
                            UserManager<IdentityUser> userManager)
        {
            _mealService = mealService;
            _mapper = mapper;
            _cartBusiness = cartBusiness;
            _orderService = orderService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public List<Meal> GetMeals(SearchModel searchModel)
        {
            try
            {
                var meals = _mealService
                            .GetMeals(x => (searchModel.Content == null || x.Name.Contains(searchModel.Content)) &&
                                            (searchModel.Category == null || x.CategoryID == searchModel.Category));

                return meals;
            }
            catch
            {
                return new List<Meal>();
            }
        }

        public Meal GetMealById(int id)
        {
            try
            {
                return _mealService.GetMealById(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Meal> AddMeal(MealRequest request)
        {
            try
            {
                var meal = _mapper.Map<Meal>(request);
                var url = MiscHelper.UploadFile(request.Image);
                meal.URL = url;
                var newMeal = await _mealService.SaveMeal(meal);

                return newMeal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Meal> UpdateMeal(MealRequest request)
        {
            try
            {
                var meal = _mapper.Map<Meal>(request);

                var mealUpdated = await _mealService.SaveMeal(meal);

                return mealUpdated;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteMeal(int id)
        {
            try
            {
                var isSuccess = await _mealService.DeleteMeal(id);

                return isSuccess;
            }
            catch
            {
                return false;
            }
        }

        public List<MealCategory> GetMealCategorys()
        {
            try
            {
                var meals = _mealService.GetMealCategorys();

                return meals;
            }
            catch
            {
                return new List<MealCategory>();
            }
        }

        public MealCategory GetMealCategoryById(int id)
        {
            try
            {
                return _mealService.GetMealCategoryById(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<MealCategory> AddMealCategory(MealCategoryRequest request)
        {
            try
            {
                var mealCategory = _mapper.Map<MealCategory>(request);
                var newMealCategory = await _mealService.SaveMealCategory(mealCategory);

                return newMealCategory;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MealCategory> UpdateMealCategory(MealCategoryRequest request)
        {
            try
            {
                var mealCategory = _mapper.Map<MealCategory>(request);

                var mealCategoryUpdated = await _mealService.SaveMealCategory(mealCategory);

                return mealCategoryUpdated;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteMealCategory(int id)
        {
            try
            {
                var isSuccess = await _mealService.DeleteMealCategory(id);

                return isSuccess;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> OrderMeal()
        {
            try
            {
                var cartItems = _cartBusiness.GetItems();

                if (cartItems is null || !cartItems.Any())
                {
                    return false;
                }

                var userName = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? string.Empty;
                var identityUser = _userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
                var user = _userService.GetByAccountId(identityUser?.Id ?? string.Empty);

                if (user is null)
                {
                    return false;
                }

                var userMealOrder = new UserMealOrder()
                {
                    UserID = user.ID,
                    Status = (int)Enums.OrderMealStatus.OrderPlace,
                    CreatedDate = DateTime.Now
                };

                var mealIds = cartItems.Select(x => x.MealId).ToList();
                var meals = _mealService.GetMeals(x => mealIds.Contains(x.ID));
                Decimal totalPrice = 0;
                var orderMeals = new List<OrderMeal>();

                foreach (var item in cartItems)
                {
                    var orderMeal = new OrderMeal()
                    {
                        OrderID = userMealOrder.ID,
                        MealID = item.MealId,
                        Amount = item.Amount,
                        Order = userMealOrder
                    };

                    orderMeals.Add(orderMeal);
                    var meal = meals.Find(x => x.ID == item.MealId);
                    totalPrice += orderMeal.Amount * (meal?.Price ?? 0);
                }

                userMealOrder.TotalPrice = totalPrice;

                var isSucess = await _orderService.OrderMeal(orderMeals);

                if (isSucess)
                {
                    _cartBusiness.DeleteCart();
                }

                return isSucess;
            }
            catch
            {
                return false;
            }
        }

        public List<UserMealOrder> GetUserMealOrders(SearchModel searchModel)
        {
            try
            {
                var model = _orderService.GetUserMealOrders(searchModel);

                if (model is null)
                {
                    model = new List<UserMealOrder>();
                }

                return model;
            }
            catch
            {
                return new List<UserMealOrder>();
            }
        }

        public async Task<bool> ChangeOrderMealStatus(int id, int status)
        {
            try
            {
                var isSuccess = await _orderService.ChangeOrderMealStatus(id, status);
                return isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UserMealOrder> GetUserMealOrdersByUser(SearchModel searchModel)
        {
            try
            {
                var userName = _httpContextAccessor.HttpContext.User?.Identity?.Name ?? string.Empty;
                var identityUser = _userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
                var user = _userService.GetByAccountId(identityUser?.Id ?? string.Empty);

                var model = _orderService.GetUserMealOrdersByUserID(searchModel, user?.ID ?? 0);

                if (model is null)
                {
                    model = new List<UserMealOrder>();
                }

                return model;
            }
            catch
            {
                return new List<UserMealOrder>();
            }
        }

        public UserMealOrder GetUserOrderMealByID(int id)
        {
            try
            {
                var userMealOrder = _orderService.GetUserOrderMealByID(id);
                return userMealOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}