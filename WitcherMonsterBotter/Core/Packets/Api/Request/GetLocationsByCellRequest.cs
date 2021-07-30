using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class GetLocationsByCellRequest : ApiRequest
    {
        [SerializableProperty]
        public List<ulong> _cellIds { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetLocationsByCell;
        }
    }
}
