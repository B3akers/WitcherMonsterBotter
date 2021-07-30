using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetKilledMonsterInstancesResponse : ApiResponse
    {
        [SerializableProperty]
        public List<long> KilledMonsterInstances { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetKilledMonsterInstances;
        }
    }
}
