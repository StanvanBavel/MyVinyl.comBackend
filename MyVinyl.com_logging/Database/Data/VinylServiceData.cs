namespace MyVinyl.com_logging.Database.Data
{
    public class VinylServiceData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
