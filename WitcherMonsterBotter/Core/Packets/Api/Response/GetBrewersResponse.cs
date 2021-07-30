using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetBrewersResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public List<Brewer> Brewers { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetBrewers;
        }
    }
}
