using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class ClaimRecipeResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public long BrewerInstanceId { get; set; }

        [SerializableProperty]
        public List<Item> Items { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.ClaimRecipe;
        }
    }
}
