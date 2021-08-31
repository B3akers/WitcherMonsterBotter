using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class AddDailyContractResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public int Contract { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.AddDailyContract;
        }
    }
}
