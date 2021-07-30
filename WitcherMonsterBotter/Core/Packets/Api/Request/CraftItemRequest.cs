using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class CraftItemRequest : ApiRequest
    {
        [SerializableProperty]
        public long BrewerInstanceId { get; set; }

        [SerializableProperty]
        public int RecipeId { get; set; }

        [SerializableProperty]
        public int ItemType { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.CraftItem;
        }
    }
}
