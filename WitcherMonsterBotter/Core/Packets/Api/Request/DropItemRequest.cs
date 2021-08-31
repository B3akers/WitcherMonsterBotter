using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class DropItemRequest : ApiRequest
    {
        [SerializableProperty]
        public int ItemId { get; set; }

        [SerializableProperty]
        public int Amount { get; set; }

        [SerializableProperty]
        public int ItemType { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.DropItem;
        }
    }
}
