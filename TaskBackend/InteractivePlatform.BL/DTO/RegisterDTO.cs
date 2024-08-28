using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InteractivePlatform.BL.DTO
{
    public class RegisterDTO
    {
        

        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
       
        public IFormFile? ProfilePicture{ get; set; }
       

    }
}
