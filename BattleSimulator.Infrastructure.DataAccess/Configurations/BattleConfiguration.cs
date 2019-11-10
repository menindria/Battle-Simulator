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
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(x => x.Logs)
                .WithOne(x => x.Battle)
                .IsRequired();

            builder.HasMany(x => x.Armies)
                .WithOne(x => x.Battle)
                .HasForeignKey(x => x.BattleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
