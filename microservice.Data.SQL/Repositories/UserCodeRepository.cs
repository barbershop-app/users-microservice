using microservice.Core.IRepositories;
using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.SQL.Repositories
{
    public class UserCodeRepository : Repository<UserCode>, IUserCodeRepository
    {
        private UsersContext? _context { get { return Context as UsersContext; } }
        public UserCodeRepository(UsersContext context) : base(context)
        {

        }
        public UserCode GetByIdIncluded(Guid id)
        {
            return _context.UserCode.Where(s => s.Id == id).FirstOrDefault();
        }

        public IEnumerable<UserCode> GetAllAsQueryable()
        {
            return _context.UserCode.AsQueryable();
        }
    }
}
