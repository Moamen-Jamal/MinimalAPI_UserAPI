using SAQAYA.UserAPI.Entities.Entities;
using SAQAYA.UserAPI.Repositories;

namespace Repositories
{
    public class UnitOfWork
    {
        private EntitiesContext context;
        public Generic<User> UserRepo { get; set; }
        public UnitOfWork(
            EntitiesContext _context,
            Generic<User> userRepo
            )
        {
            context = _context;
            UserRepo = userRepo;
            UserRepo.Context = context;
        }
        public void Commit()
        {
            context.SaveChanges();
        }

    }
}
