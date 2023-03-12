using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.ToDos;

namespace ToDoApp.Persistence.Configurations
{
    public class ToDoConfiguration : IEntityTypeConfiguration<ToDoEntity>
    {
        public void Configure(EntityTypeBuilder<ToDoEntity> builder)
        {
            //builder.ToTable("ToDo");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TargetCompletionDate).HasColumnType("date");
            builder.Property(x => x.ToDoStatus).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.ModifiedAt).IsRequired().HasColumnType("datetime");

            builder.HasOne(x => x.User).WithMany(x => x.ToDos).HasForeignKey(x=> x.OwnerId);
        }
    }
}
