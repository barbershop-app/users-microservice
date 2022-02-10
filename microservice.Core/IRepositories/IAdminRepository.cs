using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Core.IRepositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GetByIdIncluded(Guid id);
        IEnumerable<Admin> GetAllAsQueryable();
    }
}
