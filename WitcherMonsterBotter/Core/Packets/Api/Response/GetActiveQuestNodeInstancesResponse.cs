using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetActiveQuestNodeInstancesResponse : ApiResponse
    {
        [SerializableProperty]
        public List<Location> Locations { get; set; }
        [SerializableProperty]
        public List<QuestNodeInstance> QuestNodeInstances { get; set; }
        [SerializableProperty]
        public Dictionary<long, int> ExpiringQuestNodeInstances { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetActiveQuestNodeInstances;
        }
    }
}
