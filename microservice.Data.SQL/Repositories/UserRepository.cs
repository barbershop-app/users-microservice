using microservice.Core.IRepositories;
using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.SQL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private UsersContext? _context { get { return Context as UsersContext; } }
        public UserRepository(UsersContext context) : base(context)
        {

        }
        public User GetByIdIncluded(Guid id)
        {
            return _context.Users.Where(s => s.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetAllAsQueryable()
        {
            return _context.Users.AsQueryable();
        }
    }
}
