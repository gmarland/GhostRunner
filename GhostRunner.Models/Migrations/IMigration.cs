using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models.Migrations
{
    public interface IMigration
    {
        String Version { get; }

        void Up();

        void UpdateVersion();
    }
}
