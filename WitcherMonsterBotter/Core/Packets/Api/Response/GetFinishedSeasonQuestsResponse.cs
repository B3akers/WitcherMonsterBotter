using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetFinishedSeasonQuestsResponse : ApiResponse
    {
        [SerializableProperty]
        public int CurrentSeason { get; set; }

        [SerializableProperty]
        public HashSet<int> FinishedQuests { get; set; }

        [SerializableProperty]
        public int TrackedQuestId { get; set; }

        [SerializableProperty]
        public List<int> ActiveQuestIdList { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetFinishedSeasonQuests;
        }
    }
}
