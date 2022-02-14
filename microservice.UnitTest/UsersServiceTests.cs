using microservice.Core;
using microservice.Core.IServices;
using microservice.Data.Access.Services;
using microservice.Data.SQL;
using microservice.Infrastructure.Entities.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace microservice.UnitTest
{
    [TestClass]
    public class UsersServiceTests
    {
        readonly IUsersService _usersService;

        public UsersServiceTests()
        {
            string ConnectionString = "Data Source=HAKAM\\SQLEXPRESS;Initial Catalog=UsersContext;Integrated Security=True";
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(); 
            var options = builder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("microservice.Data.SQL")).Options;
            var _context = new UsersContext(options);
            IUnitOfWork _unitOfWork = new UnitOfWork(_context);

            _usersService = new UsersService(_unitOfWork);
        }

        [TestMethod]
        public void EraseFromDatabase_UserCompletlyErasedFromDatabase_ReturnsTrue()
        {
            //Arrange
            var user = new User() { FirstName = "test", LastName = "test", PhoneNumber = "1234567890" };
            _usersService.Create(user);

            //Act
            var res = _usersService.EraseFromDatabase(user);

            //Assert
            Assert.AreEqual(true, res);
        }


        [TestMethod]
        public void Create_ANewUserHasBeenCreated_ReturnsTrue()
        {
            //Arrange
            var user = new User() { FirstName = "test", LastName = "test", PhoneNumber = "1234567890" };

            //Act
            var res = _usersService.Create(user);

            //Assert
            Assert.AreEqual(true, res);
            _usersService.EraseFromDatabase(user);
        }

        [TestMethod]
        public void Update_UserInformationHasBeenUpdated_ReturnsTrue()
        {
            //Arrange
            var user = new User() { FirstName = "test", LastName = "test", PhoneNumber = "1234567890" };
            _usersService.Create(user);


            //Act
            var phoneNumber = "1111111111";
            user.PhoneNumber = phoneNumber;
            var res = _usersService.Update(user);

            //Assert
            Assert.AreEqual(true, res);
            _usersService.EraseFromDatabase(user);
        }

        [TestMethod]
        public void Delete_UserPropertyIsActiveHasBeenSetToFalse_ReturnsTrue()
        {
            //Arrange
            var user = new User() { FirstName = "test", LastName = "test", PhoneNumber = "1234567890" };
            _usersService.Create(user);

            //Act
            var res = _usersService.Delete(user);

            //Assert
            Assert.AreEqual(true, res);
            _usersService.EraseFromDatabase(user);
        }



    }
}