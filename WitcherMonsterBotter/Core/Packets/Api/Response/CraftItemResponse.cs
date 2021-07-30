using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class CraftItemResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public long BrewerInstanceId { get; set; }

        [SerializableProperty]
        public int UsesLeft { get; set; }

        [SerializableProperty]
        public int WorkingRecipe { get; set; }

        [SerializableProperty]
        public int FinishTime { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.CraftItem;
        }
    }
}
