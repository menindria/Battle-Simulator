using BattleSimulator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleSimulator.Infrastructure.DataAccess.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder
                .HasOne(x => x.OffensiveArmy)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.OffensiveArmyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.DefensiveArmy)
                .WithMany()
                .HasForeignKey(x => x.DefensiveArmyId)
                .IsRequired(false);
        }
    }
}