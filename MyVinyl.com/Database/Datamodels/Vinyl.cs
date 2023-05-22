using System;

namespace MyVinyl.com.Database.Datamodels
{
    public class Vinyl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
