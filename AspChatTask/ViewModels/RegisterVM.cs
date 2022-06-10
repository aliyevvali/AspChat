using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspChatTask.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string  UserName{ get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public IFormFile Image  { get; set; }
    }
}
