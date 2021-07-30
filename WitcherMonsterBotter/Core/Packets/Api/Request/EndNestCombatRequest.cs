using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class EndNestCombatRequest : ApiRequest
    {
        [SerializableProperty]
        public bool Win { get; set; }

        [SerializableProperty]
        public long InstanceId { get; set; }

        [SerializableProperty]
        public int[] DetailsFirst { get; set; }

        [SerializableProperty]
        public int[] DetailsSecond { get; set; }

        [SerializableProperty]
        public int[] DetailsBoss { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.EndNestCombat;
        }
    }
}
