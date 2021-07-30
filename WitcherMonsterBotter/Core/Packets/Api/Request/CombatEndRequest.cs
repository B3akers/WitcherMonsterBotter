using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class CombatEndRequest : ApiRequest
    {
        [SerializableProperty]
        public bool Win { get; set; }
        [SerializableProperty]
        public int[] Details { get; set; }
        [SerializableProperty]
        public bool PlayerSurrendered { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.CombatEnd;
        }
    }
}
