using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class ClaimMonsterKnowledgeRewardResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public int MonsterId { get; set; }

        [SerializableProperty]
        public int SkillPoints { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.ClaimMonsterKnowledgeReward;
        }
    }
}
