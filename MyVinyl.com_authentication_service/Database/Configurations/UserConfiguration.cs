using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinyl.com_authentication_service.Database.Datamodels;


namespace MyVinyl.com_authentication_service.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Name").HasMaxLength(255);
            builder.Property<string>("Email").IsRequired();
            builder.Property<string>("Email").HasMaxLength(255);
            builder.Property<string>("Phonenumber").IsRequired();
            builder.Property<string>("Phonenumber").HasMaxLength(int.MaxValue);
           
        }
    }
}
