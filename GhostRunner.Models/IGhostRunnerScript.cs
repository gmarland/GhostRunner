using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Models
{
    public interface IGhostRunnerScript
    {
        String ExternalId { get; }

        String Name { get; }

        String Description { get; }

        String Content { get; }

        ScriptType Type { get; }

        Project Project { get; }

        Boolean HasParameters();

        String[] GetAllParameters();
    }
}
