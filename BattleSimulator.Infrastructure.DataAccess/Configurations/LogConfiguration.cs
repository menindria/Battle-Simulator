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
                .HasOne(x => x.ArmyOne)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.ArmyOneId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.ArmyTwo)
                .WithMany()
                .HasForeignKey(x => x.ArmyTwoId)
                .IsRequired(false);
        }
    }
}