using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetWeeklyContractProgressResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public int LastStampAcquiredDate { get; set; }

        [SerializableProperty]
        public List<int> Stamps { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetWeeklyContractProgress;
        }
    }
}
