using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithOne(x => x.bookingEntity)
            .HasForeignKey<BookingEntity>(x => x.UserEntityId);
        
        builder.HasOne(x => x.Room)
            .WithOne(x => x.BookingEntity)
            .HasForeignKey<BookingEntity>(x => x.RoomEntityId);
    }
}