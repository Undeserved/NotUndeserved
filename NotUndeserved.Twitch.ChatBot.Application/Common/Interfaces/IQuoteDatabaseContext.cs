using Microsoft.EntityFrameworkCore;
using NotUndeserved.Twitch.ChatBot.Domain.Entities;
using NotUndeserved.Twitch.ChatBot.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces {
    public interface IQuoteDatabaseContext {
        //Tables
        DbSet<Quote> Quotes {  get; set; }
        DbSet<QuoteLog> QuoteLogs {  get; set; }
        DbSet<TwitchApiSettings> Settings {  get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
