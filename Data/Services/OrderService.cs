using Core.Models.Requests;
using Data.Entities;
using Data.Repositories;
using Data.UnitOfWorks;

namespace Data.Services
{
    public interface IOrderService
    {
        Task<bool> OrderMeal(List<OrderMeal> orderMeals);
        List<UserMealOrder> GetUserMealOrders(SearchModel searchModel);
        Task<bool> ChangeOrderMealStatus(int id, int status);
        List<UserMealOrder> GetUserMealOrdersByUserID(SearchModel searchModel, int userId);
        UserMealOrder GetUserOrderMealByID(int id);
    }
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<OrderMeal> _orderMealRepository;
        private readonly IGenericRepository<UserMealOrder> _userMealOrderRepository;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderMealRepository = unitOfWork.Repository<OrderMeal>();
            _userMealOrderRepository = unitOfWork.Repository<UserMealOrder>();
        }

        public async Task<bool> ChangeOrderMealStatus(int id, int status)
        {
            try
            {
                var order = _userMealOrderRepository.GetById(id);
                order.Status = status;
                _userMealOrderRepository.Update(order);
                var row = await _unitOfWork.CompleteAsync();

                if (row <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserMealOrder GetUserOrderMealByID(int id)
        {
            try
            {
                var userMealOrder = _userMealOrderRepository.Find(filter: x=>x.ID == id, includeProperties: "User,OrderMeals,OrderMeals.Meal").FirstOrDefault();
                return userMealOrder;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<UserMealOrder> GetUserMealOrders(SearchModel searchModel)
        {
            try
            {
                var model = _userMealOrderRepository.Find(filter: x => (searchModel.Content == null || x.User.Name.Contains(searchModel.Content)) &&
                                                                        (searchModel.Status == null || x.Status == searchModel.Status),
                                                        includeProperties: "User,OrderMeals,OrderMeals.Meal").ToList();
                return model;
            }
            catch (Exception)
            {
                return new List<UserMealOrder>();
            }
        }

        public List<UserMealOrder> GetUserMealOrdersByUserID(SearchModel searchModel, int userId)
        {
            try
            {
                var model = _userMealOrderRepository.Find(filter: x => (searchModel.Content == null || x.User.Name.Contains(searchModel.Content)) &&
                                                                        (searchModel.Status == null || x.Status == searchModel.Status) && x.User.ID == userId,
                                                        includeProperties: "User,OrderMeals,OrderMeals.Meal").ToList();
                return model;
            }
            catch (Exception)
            {
                return new List<UserMealOrder>();
            }
        }

        public async Task<bool> OrderMeal(List<OrderMeal> orderMeals)
        {
            try
            {
                _orderMealRepository.AddRange(orderMeals);
                var row = await _unitOfWork.CompleteAsync();

                if (row <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
