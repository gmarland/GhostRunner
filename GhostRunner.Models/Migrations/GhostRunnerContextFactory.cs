using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models.Migrations
{
    public class GhostRunnerContextFactory : IDbContextFactory<GhostRunnerContext>
    {
        public GhostRunnerContext Create()
        {
            return new GhostRunnerContext("DatabaseConnectionString");
        }
    }
}
