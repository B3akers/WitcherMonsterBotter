using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class GatherHerbRequest : ApiRequest
    {
        [SerializableProperty]
        public long InstanceId { get; set; }
      
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GatherHerb;
        }
    }
}
