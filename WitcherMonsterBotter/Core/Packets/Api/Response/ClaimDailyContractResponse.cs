using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class ClaimDailyContractResponse : ApiResponse
    {
        [SerializableProperty]
        public int First { get; set; }

        [SerializableProperty]
        public int Second { get; set; }

        [SerializableProperty]
        public int Third { get; set; }

        public bool Success
        {
            get
            {
                return First == 0;
            }
        }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.ClaimDailyQuest;
        }
    }
}
