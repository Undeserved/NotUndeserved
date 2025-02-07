using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence.Configurations {
    public class TwitchApiSettingsConfiguration : IEntityTypeConfiguration<TwitchApiSettings> {
        public void Configure(EntityTypeBuilder<TwitchApiSettings> builder) {
            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.ClientId)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.ClientSecret)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.RefreshToken)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.TwitchAccount)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(x => x.TargetChannel)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(x => x.QuoteAuthority)
                .HasMaxLength(32)
                .IsRequired();
            builder.Property(x => x.OAuthToken)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.TokenValidUntil)
                .IsRequired();
        }
    }
}
