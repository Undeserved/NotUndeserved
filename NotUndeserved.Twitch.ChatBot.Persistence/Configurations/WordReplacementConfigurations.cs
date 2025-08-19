using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence.Configurations {
    public class WordReplacementConfigurations : IEntityTypeConfiguration<WordReplacement> {
        public void Configure(EntityTypeBuilder<WordReplacement> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired();
            builder.Property(x => x.OriginalText)
                .IsRequired();
            builder.Property(x => x.ReplacementText)
                .IsRequired();
        }
    }
}
