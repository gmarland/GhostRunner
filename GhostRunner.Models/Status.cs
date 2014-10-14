using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostRunner.Models
{
    public enum Status
    {
        Unprocessed,
        Processing,
        Completed,
        Errored,
        Unknown
    }
}
