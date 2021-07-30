using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class DailyContractCompletedResponse : ApiResponse
    {
        [SerializableProperty]
        public int Contract { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.DailyContractCompleted;
        }
    }
}
