using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetAllFactsResponse : ApiResponse
    {
        [SerializableProperty]
        public Dictionary<int, int> Facts { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetAllFacts;
        }
    }
}
