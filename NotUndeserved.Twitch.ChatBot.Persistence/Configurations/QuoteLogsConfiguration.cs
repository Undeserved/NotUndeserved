using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence.Configurations {
    public class QuoteLogsConfiguration : IEntityTypeConfiguration<QuoteLog> {
        public void Configure(EntityTypeBuilder<QuoteLog> builder) {
            builder.HasKey(x => x.LogEntry);

            builder.Property(x => x.LogEntry)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(x => x.QuoteId)
                .IsRequired();
            builder.Property(x => x.RequestDate)
                .IsRequired();
        }
    }
}
