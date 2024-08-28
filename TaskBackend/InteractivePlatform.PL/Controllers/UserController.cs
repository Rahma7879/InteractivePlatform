using InteractivePlatform.BL.DTO;
using InteractivePlatform.BL.UOW;
using InteractivePlatform.DAL.Model;
using InteractivePlatform.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteractivePlatform.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UnitOfWorks unitOfWork;

        IFileService fileService;

        public UserController(UnitOfWorks unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork; 

            this.fileService = fileService;
        }
        [HttpPost]
        public async Task<ActionResult> AddUserAsync(UserAddDTO useraddDTO)
        {
            string[] allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (useraddDTO == null)
            {
                return BadRequest();
            }

            string createdImageName = await fileService.SaveFileAsync(useraddDTO.ProfilePicture, allowedFileExtensions);

            User user = new User
            {

                Email = useraddDTO.Email,
                Password = useraddDTO.Password,
                ProfilePicture = createdImageName,
                FirstName = useraddDTO.FirstName,
                LastName = useraddDTO.LastName,

                RoleId = 1,
            };

            unitOfWork.UsersRepository.add(user);
            unitOfWork.savechanges();
            return Ok(useraddDTO);
        }

    }
}
