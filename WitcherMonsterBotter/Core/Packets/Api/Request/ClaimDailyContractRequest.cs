using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Request
{
    public class ClaimDailyContractRequest : ApiRequest
    {
        [SerializableProperty]
        public int Param { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.ClaimDailyQuest;
        }
    }
}
