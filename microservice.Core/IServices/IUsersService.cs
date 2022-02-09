using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Core.IServices
{
    public interface IUsersService
    {
        User GetById(Guid id);
        IEnumerable<User> GetAllAsQueryable();
        public bool Create(User user);
        public bool Update(User user);
        public bool Delete(User user);

        public bool SetAuthenticationCode(string code, User user);
        public bool AuthenticateCode(string code, User user);
    }
}
