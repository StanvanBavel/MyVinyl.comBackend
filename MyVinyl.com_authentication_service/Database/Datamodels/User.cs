using System;

namespace MyVinyl.com_authentication_service.Database.Datamodels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
