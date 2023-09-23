using AutoMapper;
using Core.Helper;
using Core.Models;
using Core.Models.Requests;
using Data.Entities;
using Data.Services;
using Microsoft.Extensions.Configuration;

namespace Business.Restaurant
{
    public interface ICartBusiness
    {
        Cart GetCart();
        List<CartItem> GetItems();
        bool AddToCart(int id, int? amount = null);
        bool DeleteCartItem(int id);
        bool DeleteCart();
    }
    public class CartBusiness : ICartBusiness
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        private readonly ISessionHelper _sessionHelper;
        private readonly string _key;

        public CartBusiness(IMealService mealService,
                            IMapper mapper,
                            ISessionHelper sessionHelper,
                            IConfiguration configuration)
        {
            _mealService = mealService;
            _mapper = mapper;
            _sessionHelper = sessionHelper;
            _key = configuration["SessionKey:CartKey"];
        }

        public Cart GetCart()
        {
            try
            {
                var items = GetItems();
                var mealIds = items.Select(x => x.MealId).ToList();
                var meals = _mealService.GetMeals(x => mealIds.Contains(x.ID));

                if (meals is null)
                {
                    meals = new List<Meal>();
                }

                return new Cart { CartItems = items, Meals = _mapper.Map<List<MealRequest>>(meals) };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CartItem> GetItems()
        {
            try
            {
                var cartItems = _sessionHelper.GetSession<List<CartItem>>(_key);

                if (cartItems is null)
                {
                    cartItems = new List<CartItem>();
                }

                return cartItems;
            }
            catch (Exception)
            {
                return new List<CartItem>();
            }
        }

        public bool AddToCart(int id, int? amount = null)
        {
            try
            {
                if (amount is not null && amount < 0)
                {
                    return false;
                }

                var cartItems = _sessionHelper.GetSession<List<CartItem>>(_key);

                if (cartItems is null)
                {
                    cartItems = new List<CartItem>();
                }

                var isSuccess = false;
                var item = cartItems.FirstOrDefault(x => x.MealId == id);

                if (item == null)
                {
                    cartItems.Add(new CartItem() { MealId = id, Amount = 1 });
                    isSuccess = _sessionHelper.SetSession<List<CartItem>>(_key, cartItems);
                    return isSuccess;
                }

                if (amount is null)
                {
                    item.Amount += 1;
                    isSuccess = _sessionHelper.SetSession<List<CartItem>>(_key, cartItems);
                    return isSuccess;
                }

                item.Amount = amount ?? 0;
                isSuccess = _sessionHelper.SetSession<List<CartItem>>(_key, cartItems);
                return isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartItem(int id)
        {
            try
            {
                var cartItems = _sessionHelper.GetSession<List<CartItem>>(_key);

                if (cartItems is null)
                {
                    return false;
                }

                var item = cartItems.FirstOrDefault(x => x.MealId == id);

                if (item == null)
                {
                    return false;
                }

                cartItems.Remove(item);
                var isSuccess = _sessionHelper.SetSession<List<CartItem>>(_key, cartItems);
                return isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCart()
        {
            try
            {
                var isSuccess = _sessionHelper.SetSession<List<CartItem>>(_key, new List<CartItem>());
                return isSuccess;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}