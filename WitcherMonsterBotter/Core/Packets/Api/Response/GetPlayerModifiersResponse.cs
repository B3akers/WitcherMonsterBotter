using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetPlayerModifiersResponse : ApiResponse
    {
        [SerializableProperty]
        public int Result { get; set; }

        [SerializableProperty]
        public List<ExpiringPlayerModifier> Modifiers { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetPlayerModifiers;
        }
    }
}
