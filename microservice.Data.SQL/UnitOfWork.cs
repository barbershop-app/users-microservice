using microservice.Core;
using microservice.Core.IRepositories;
using microservice.Data.SQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext _context;
        public UnitOfWork(UsersContext context)
        {
            this._context = context;
        }

        private IUserRepository _userRepository;
        private IUserCodeRepository _usersCodesRepository;



        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IUserCodeRepository UsersCodes => _usersCodesRepository ??= new UserCodeRepository(_context);


        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
