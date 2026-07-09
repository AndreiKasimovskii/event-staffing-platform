using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventStaffingPlatform.Host.Events;

public class EventEntityConfiguration : IEntityTypeConfiguration<EventStorageEntity>
{
	public void Configure(EntityTypeBuilder<EventStorageEntity> builder)
	{
		builder.ToTable("events");

		builder.Property(e => e.Id)
			.HasColumnName("event_id");

		builder.Property(e => e.Title)
			.HasColumnName("title")
			.HasMaxLength(512)
			.IsRequired();

		builder.Property(e => e.Description)
			.HasColumnName("description");

		builder.Property(e => e.Address)
			.HasColumnName("address")
			.HasMaxLength(128)
			.IsRequired();

		builder.Property(e => e.StartEventDate)
			.HasColumnName("start_event_date")
			.IsRequired();

		builder.Property(e => e.EndEventDate)
			.HasColumnName("end_event_date")
			.IsRequired();
	}
}