namespace Confab.Modules.Identity.Core.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Confab.Modules.Identity.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Claims)
                .HasConversion(x => JsonSerializer.Serialize(x, SerializerOptions),
                    x => JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(x, SerializerOptions));

            builder.Property(x => x.Claims).Metadata.SetValueComparer(
                new ValueComparer<Dictionary<string, IEnumerable<string>>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary(x => x.Key, x => x.Value)));
        }
    }
}