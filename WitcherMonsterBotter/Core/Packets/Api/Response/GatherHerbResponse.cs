using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GatherHerbResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public List<int> Loot { get; set; }

        [SerializableProperty]
        public int RespawnTime { get; set; }

        [SerializableProperty]
        public long HerbInstanceId { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GatherHerb;
        }
    }
}
