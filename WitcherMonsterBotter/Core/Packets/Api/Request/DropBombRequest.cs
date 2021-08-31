using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class DropBombRequest : ApiRequest
    {
        [SerializableProperty]
        public int ItemId { get; set; }

        [SerializableProperty]
        public int Amount { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.DropBomb;
        }
    }
}
