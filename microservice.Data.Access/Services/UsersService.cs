using microservice.Core;
using microservice.Core.IServices;
using microservice.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.Access.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetById(Guid Id)
        {
            return _unitOfWork.Users.GetByIdIncluded(Id);
        }


        public IEnumerable<User> GetAllAsQueryable()
        {
            return _unitOfWork.Users.GetAllAsQueryable();
        }


        public bool Create(User user)
        {
            user.CreateDate = DateTime.Now;
            user.IsActive = true;
            _unitOfWork.Users.Add(user);
            return _unitOfWork.Commit() > 0;
        }

        public bool Update(User oldUser, User user)
        {
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            _unitOfWork.Users.Update(oldUser);
            return _unitOfWork.Commit() > 0;

        }
        public bool Delete(User user)
        {
            user.IsActive = false;
            _unitOfWork.Users.Update(user);
            return _unitOfWork.Commit() > 0;
        }

        public bool EraseFromDatabase(User user)
        {
            _unitOfWork.Users.Remove(user);
            return _unitOfWork.Commit() > 0;
        }



        public bool SetAuthenticationCode(string code, User user)
        {
            var userCode = _unitOfWork.UsersCodes.Where(x => x.UserId == user.Id).FirstOrDefault();

            //Create usercode if doesn't exist otherwise update the current one with a new code 
            if (userCode == null)
            {
                userCode = new UserCode() { UserId = user.Id, Code = code};
                _unitOfWork.UsersCodes.Add(userCode);
            }
            else
            {
                userCode.Code = code;
                _unitOfWork.UsersCodes.Update(userCode);
            }

            return _unitOfWork.Commit() > 0;
        }

        public bool AuthenticateCode(string code, User user)
        {
            //Check if code is correct
            var userCode = _unitOfWork.UsersCodes.Where(x => x.Code == code && x.UserId == user.Id).FirstOrDefault();
            return userCode != null;
        }

        public bool UserIsAdmin(Guid id)
        {
            var admin = _unitOfWork.Admins.GetAllAsQueryable().Where(x => x.UserId == id).FirstOrDefault();
            return admin != null;
        }


    }
}
