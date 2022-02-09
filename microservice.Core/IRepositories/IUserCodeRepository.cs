using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Core.IRepositories
{
    public interface IUserCodeRepository: IRepository<UserCode>
    {
        UserCode GetByIdIncluded(Guid id);
        IEnumerable<UserCode> GetAllAsQueryable();

    }
}
