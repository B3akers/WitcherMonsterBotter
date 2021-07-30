using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetSensedMonstersResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public List<long> SensedMonsters { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetSensedMonsters;
        }
    }
}
