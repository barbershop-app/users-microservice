using microservice.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Core
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserCodeRepository UsersCodes { get; }

        int Commit();
    }
}
