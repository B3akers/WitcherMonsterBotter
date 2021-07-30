using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetKnownRecipesResponse : ApiResponse
    {
        [SerializableProperty]
        public Dictionary<int, HashSet<int>> KnownRecipes { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetKnownRecipes;
        }
    }
}
