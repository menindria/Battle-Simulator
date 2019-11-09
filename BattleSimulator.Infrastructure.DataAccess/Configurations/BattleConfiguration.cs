using BattleSimulator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleSimulator.Infrastructure.DataAccess.Configurations
{
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(255);

            builder.HasMany(x => x.Logs)
                .WithOne(x => x.Battle)
                .IsRequired();
        }
    }
}
