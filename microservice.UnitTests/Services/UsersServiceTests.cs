using microservice.Core.IServices;
using microservice.Infrastructure.Entities.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.UnitTests.Services
{
    [TestClass]
    public class UsersServiceTests
    {
        readonly IUsersService _usersService;

        public UsersServiceTests(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [TestMethod]
        public void CreateUser_ANewUserHasBeenCreated_ReturnsTrue()
        {
            //Arrange
            var user = new User() { FirstName = "Hakam", LastName = "Mssarwe", PhoneNumber = "1234567890"};

            //Act
            var res = _usersService.Create(user);

            //Assert
            Assert.AreEqual(true, res);
        }

    }
}
