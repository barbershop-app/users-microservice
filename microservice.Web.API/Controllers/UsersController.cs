using AutoMapper;
using microservice.Core.IServices;
using microservice.Infrastructure.Entities.DB;
using microservice.Infrastructure.Entities.DTOs;
using microservice.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace microservice.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly ILogger<UsersController> _logger;
        readonly IUsersService _usersService;
        readonly IServiceScopeFactory _serviceScopeFactory;
        readonly IHttpClientService _httpClientService;

        public UsersController(IMapper mapper, ILogger<UsersController> logger, IUsersService usersService, IServiceScopeFactory serviceScopeFactory, IHttpClientService httpClientService)
        {
            _mapper = mapper;
            _logger = logger;
            _usersService = usersService;
            _serviceScopeFactory = serviceScopeFactory;
            _httpClientService = httpClientService;
        }

        [HttpGet]
        [Route("IsActive/{id}")]
        public IActionResult UserIsActive(Guid id)
        {
            var user = _usersService.GetById(id);

            if (user != null && user.IsActive)
                return Ok(new {message = "User is active" });


            return BadRequest(new { message = "User is not active" });
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var user = _usersService.GetById(id);

                if (user == null)
                    return BadRequest(new { message = "User does not exist." });


                return Ok(new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    phoneNumber = user.PhoneNumber,
                    createDate = user.CreateDate,
                    isActive = user.IsActive
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(new { message = "Something went wrong." });
            }
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] UserDTOs.Create dto)
        {
            if (dto?.PhoneNumber.Length != 10 || dto?.PhoneNumber.StartsWith("05") == false)
                return BadRequest(new {message = "Invalid Phone Number." });

            try
            {
                var user = _usersService.GetAllAsQueryable().Where(x => x.PhoneNumber == dto.PhoneNumber).FirstOrDefault();

                if (user != null && !user.IsActive)
                    return BadRequest(new { message = "This phone number is disabled." });

                var res = true;

                //Add the user to the database if doesn't exist. 
                if (user == null)
                {
                    user = _mapper.Map<User>(dto);
                    res = _usersService.Create(user);
                }

                if (res)
                {
                    res = _usersService.SetAuthenticationCode("5555", user);

                    //Generate code
                    //string code = StaticFunctions.GenerateVerificationCode();

                    //res = _usersService.SetAuthenticationCode(code, user);

                    if (res)
                    {
                        //Send SMS with verification code
                        //Task.Run(() =>
                        //{
                        //    using (var scope = _serviceScopeFactory.CreateScope())
                        //    {

                        //        var httpClientService = scope.ServiceProvider.GetService<IHttpClientService>();

                        //        httpClientService.SendSMS(user.PhoneNumber, code);
                        //    }

                        //});


                        return Ok(new { message = "The Verficitaion code is being sent." });
                    }
                }

                return BadRequest(new { message = "Failed to create user." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] UserDTOs.Update dto)
        {
            try
            {
                var oldUser = _usersService.GetById(dto.Id);

                if (oldUser == null)
                    return BadRequest(new { message = "User does not exist." });


                var user = _mapper.Map<User>(dto);

                var res = _usersService.Update(oldUser, user);

                if (res)
                    return Ok(new { message = "User details have been updated." });

                return BadRequest("Failed to update user details.");

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var tempUser = _usersService.GetById(id);

                if (tempUser == null)
                    return BadRequest(new { message = "User does not exist." });


                if (!tempUser.IsActive)
                    return BadRequest(new { message = "User already deleted." });

                var res = _usersService.Delete(tempUser);

                if (res)
                    return Ok(new { message = "User has been deleted." });


                return BadRequest(new { message = "User has not been deleted." });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] UserDTOs.Authenticate dto)
        { 
            try
            {
                var user = _usersService.GetAllAsQueryable().Where(x => x.PhoneNumber == dto.PhoneNumber).FirstOrDefault();

                if (user == null)
                    return BadRequest(new { message = "User does not exist." });

                var res = _usersService.AuthenticateCode(dto.code, user);

                if (res) 
                    return Ok(new
                    {
                        id = user.Id,
                        isAdmin = _usersService.UserIsAdmin(user.Id),
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        token = StaticFunctions.GenerateJwtToken(user.Id)

                    });
       

                return BadRequest(new { message = "User has not been authenticated." });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(new { message = "Something went wrong." });
            }
        }
    }
}
