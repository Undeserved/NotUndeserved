using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Persistence {
    public class QuoteDatabaseContextFactory : DesignTimeDbContextFactoryBase<QuoteDatabaseContext> {
        protected override QuoteDatabaseContext CreateNewInstance(DbContextOptions<QuoteDatabaseContext> options) {
            return new QuoteDatabaseContext(options);
        }
    }
}
