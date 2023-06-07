using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyVinyl.com_authentication_service.Database.Datamodels.Dtos
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phonenumber { get; set; }
    }
}
