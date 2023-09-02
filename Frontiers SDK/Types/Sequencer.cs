using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontiers.Types
{
    public class Sequencer : List<TransactionPacket>
    {
        public int prioritycount {  get; set; }
    }
}
