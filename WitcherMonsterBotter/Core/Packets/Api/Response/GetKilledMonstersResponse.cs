using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetKilledMonstersResponse : ApiResponse
    {
        [SerializableProperty]
        public Dictionary<int, int> KilledMonsters { get; set; }

        [SerializableProperty]
        public Dictionary<int, int> ClaimedMonsterKnowledgeTiers { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetKilledMonsters;
        }
    }
}
