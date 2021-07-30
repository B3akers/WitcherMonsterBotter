using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Utility;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class LoadCellsRequest : ApiRequest
    {
        [SerializableProperty]
        public List<ulong> _cellIds { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.LoadCells;
        }
    }
}
