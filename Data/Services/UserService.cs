using Data.Entities;
using Data.Repositories;
using Data.UnitOfWorks;

namespace Data.Services
{
    public interface IUserService
    {
        User? GetByAccountId(string accountId);
        Task<bool> AddUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.Repository<User>();
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                _userRepository.Add(user);
                var row = await _unitOfWork.CompleteAsync();

                if(row > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User? GetByAccountId(string accountId)
        {
            try
            {
                var user = _userRepository.Find(x => x.AccountId == accountId).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
