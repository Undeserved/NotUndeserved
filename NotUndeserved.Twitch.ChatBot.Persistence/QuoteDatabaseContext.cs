using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using NotUndeserved.Twitch.ChatBot.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence {
    public class QuoteDatabaseContext : DbContext, IQuoteDatabaseContext {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteLog> QuoteLogs { get; set; }
        public DbSet<TwitchApiSettings> Settings { get; set; }
        public DbSet<WordReplacement> WordReplacement { get; set; }

        public QuoteDatabaseContext(DbContextOptions<QuoteDatabaseContext> options) 
            : base(options) {
            base.Database.EnsureCreated();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuoteDatabaseContext).Assembly);
        }
    }
}
