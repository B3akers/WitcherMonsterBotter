using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetSummonedMonstersResponse : ApiResponse
    {
        [SerializableProperty]
        public List<LocalMonstersEntity> LocalMonstersEntities { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetSummonedMonsters;
        }
    }
}
