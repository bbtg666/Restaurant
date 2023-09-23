using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;

namespace Data.Repositories
{
    public interface IMealRepository : IGenericRepository<Meal>
    {
    }
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(DbContext context) : base(context)
        {
        }
    }
}
