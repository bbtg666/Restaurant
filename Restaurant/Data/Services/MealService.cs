using Core.Models;
using Data.Entities;
using Data.Repositories;
using Data.UnitOfWorks;
using System.Linq.Expressions;

namespace Data.Services
{
    public interface IMealService
    {
        List<Meal> GetMeals(Expression<Func<Meal, bool>> filter = null);
        Meal GetMealById(int id);
        Task<Meal> SaveMeal(Meal meal);
        Task<bool> DeleteMeal(int id);

        List<MealCategory> GetMealCategorys(Expression<Func<MealCategory, bool>> filter = null);
        MealCategory GetMealCategoryById(int id);
        Task<MealCategory> SaveMealCategory(MealCategory meal);
        Task<bool> DeleteMealCategory(int id);
    }
    public class MealService : IMealService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Meal> _mealRepository;
        private readonly IGenericRepository<MealCategory> _mealCategoryRepository;

        public MealService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mealRepository = unitOfWork.Repository<Meal>();
            _mealCategoryRepository = unitOfWork.Repository<MealCategory>();
        }

        public async Task<Meal> SaveMeal(Meal meal)
        {
            try
            {
                if (meal.ID > 0)
                {
                    _mealRepository.Update(meal);
                }
                else
                {
                    _mealRepository.Add(meal);
                }

                await _unitOfWork.CompleteAsync();

                return meal;
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
                var meal = _mealRepository.GetById(id);
                _mealRepository.Remove(meal);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Meal> GetMeals(Expression<Func<Meal, bool>> filter = null)
        {
            try
            {
                return _mealRepository.Find(filter).ToList();
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
                return _mealRepository.Find((x) => x.ID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MealCategory> GetMealCategorys(Expression<Func<MealCategory, bool>> filter = null)
        {
            try
            {
                return _mealCategoryRepository.Find(filter).ToList();
            }
            catch (Exception)
            {

                return new List<MealCategory>();
            }
        }

        public MealCategory GetMealCategoryById(int id)
        {
            try
            {
                return _mealCategoryRepository.GetById(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<MealCategory> SaveMealCategory(MealCategory mealCategory)
        {
            try
            {
                if (mealCategory.ID > 0)
                {
                    _mealCategoryRepository.Update(mealCategory);
                }
                else
                {
                    _mealCategoryRepository.Add(mealCategory);
                }

                await _unitOfWork.CompleteAsync();

                return mealCategory;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteMealCategory(int id)
        {
            try
            {
                var meal = _mealCategoryRepository.GetById(id);
                _mealCategoryRepository.Remove(meal);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}