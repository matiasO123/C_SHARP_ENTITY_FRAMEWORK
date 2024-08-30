using Ef_code_first.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ef_code_first.Migrations
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User() { Email = "user1@gmail.com", Id = 1, Name = "name1" },
                new User() { Email = "user3@gmail.com", Id = 3, Name = "name3" },
                new User() { Email = "user2@gmail.com", Id = 2, Name = "name2" });
        }
    }
}
