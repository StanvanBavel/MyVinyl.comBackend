using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyVinyl.com.Database.Datamodels;


namespace MyVinyl.com.Database.Configurations
{
    public class VinylConfiguration : IEntityTypeConfiguration<Vinyl>
    {
        public void Configure(EntityTypeBuilder<Vinyl> builder)
        {
            builder.ToTable("vinyls");
            builder.HasKey(x => x.Id);
            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Name").HasMaxLength(255);
            builder.Property<string>("Description").IsRequired();
            builder.Property<string>("Description").HasMaxLength(255);
            builder.Property<string>("Image").IsRequired();
            builder.Property<string>("Image").HasMaxLength(int.MaxValue);
           
        }
    }
}
