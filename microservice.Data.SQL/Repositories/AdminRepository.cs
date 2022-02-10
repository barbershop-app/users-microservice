using microservice.Core.IRepositories;
using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.SQL.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private UsersContext? _context { get { return Context as UsersContext; } }
        public AdminRepository(UsersContext context) : base(context)
        {

        }
        public Admin GetByIdIncluded(Guid id)
        {
            return _context.Admins.Where(s => s.Id == id).FirstOrDefault();
        }

        public IEnumerable<Admin> GetAllAsQueryable()
        {
            return _context.Admins.AsQueryable();
        }
    }
}
