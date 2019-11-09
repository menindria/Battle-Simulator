using BattleSimulator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleSimulator.Infrastructure.DataAccess.Configurations
{
    public class ArmyConfiguration : IEntityTypeConfiguration<Army>
    {
        public void Configure(EntityTypeBuilder<Army> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(255);
        }
    }
}