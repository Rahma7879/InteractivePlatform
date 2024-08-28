
using InteractivePlatform.BL.DTO;
using InteractivePlatform.BL.UOW;
using InteractivePlatform.DAL.Model;
using InteractivePlatform.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace InteractivePlatform.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UnitOfWorks _unitOfWork;
        private readonly IFileService fileService;

        public AccountController(UnitOfWorks unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            this.fileService = fileService;
        }
        [HttpPost("Register")]
        
        public async Task<ActionResult> RegisterAsync([FromForm] RegisterDTO userDto)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool emailChecker = false;

            List<User> existingUsers = _unitOfWork.UsersRepository.selectall();
            for (int i = 0; i < existingUsers.Count(); i++)
            {
                if (existingUsers[i].Email == userDto.Email)
                {
                    emailChecker = true;
                }
            }
            if (!emailChecker)
            {
                string[] allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };

                if (userDto == null)
                {
                    return BadRequest();
                }

                string createdImageName;
                if (userDto.ProfilePicture != null)
                {
                    createdImageName = await fileService.SaveFileAsync(userDto.ProfilePicture, allowedFileExtensions);
                }
                else
                {
                    createdImageName = null;
                }
               

                User appuser = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    ProfilePicture= createdImageName,

                    RoleId = 1
                };
                _unitOfWork.UsersRepository.add(appuser);
                _unitOfWork.savechanges();
                return Created();
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public ActionResult signin([FromBody] LoginDTO userData)
        {
            var user = _unitOfWork.UsersRepository.FindEmail(userData.Email);

            if (user != null && user.Password == userData.Password)
            {
                
                    List<Claim> Data = new List<Claim>();
                    Data.Add(new Claim("name", user.FirstName));
                    Data.Add(new Claim(ClaimTypes.MobilePhone, "0112874"));
                    Data.Add(new Claim("UserId", user.Id.ToString()));




                    if (user.Role.RoleName == "Admin")
                    {
                        Data.Add(new Claim("isAdmin", "true"));
                        Data.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    

                    string seckey = "Welcome to our First Api Website for InteractivePlatform project";
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(seckey));
                    var Signcer = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                            claims: Data,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: Signcer
                        );
                    var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(stringToken);
                }
                else
                {
                    return Unauthorized();
                }
            }

        }
    }



