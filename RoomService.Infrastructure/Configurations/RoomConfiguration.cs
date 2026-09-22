using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.User).WithOne().HasForeignKey<RoomEntity>(x => x.UserEntityId);
    }
}