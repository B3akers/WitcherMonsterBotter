using System;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetLastSummoningSkillUsageTimeResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Result { get; set; }

        [SerializableProperty]
        public int Param { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetLastSummoningSkillUsageTime;
        }
    }
}
