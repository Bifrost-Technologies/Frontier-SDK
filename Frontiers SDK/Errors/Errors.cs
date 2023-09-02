using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontiers.Errors
{
    public enum FrontierErrorKind : uint
    {
        AlreadyInitialized = 6000U,
        NotInitialized = 6001U,
        NotEnoughResources = 6002U
    }
}
