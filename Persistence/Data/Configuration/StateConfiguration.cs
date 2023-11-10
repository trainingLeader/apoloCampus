using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("state");

            builder.HasIndex(e => e.IdcountryFk, "IX_state_IdcountryFk");

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            builder.HasOne(d => d.IdcountryFkNavigation).WithMany(p => p.States)
                .HasForeignKey(d => d.IdcountryFk)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}