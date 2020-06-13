using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data {
    public class PersionMaping : IEntityTypeConfiguration<Person> {
        public void Configure(EntityTypeBuilder<Person> person) {
            person.HasKey(x => x.Id);
            person.Property(x => x.Name).IsRequired().HasMaxLength(300);
            person.Property(x => x.Family).IsRequired().HasMaxLength(300);
        }
    }
}
