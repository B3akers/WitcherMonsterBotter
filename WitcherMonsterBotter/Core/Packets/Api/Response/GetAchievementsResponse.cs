using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetAchievementsResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> Achievement { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetAchievements;
        }
    }
}
