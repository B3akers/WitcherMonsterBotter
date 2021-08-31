using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetDailyContractsResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public int Empty { get; set; }

        [SerializableProperty]
        public int CanAdd { get; set; }

        [SerializableProperty]
        public int CanReshuffle { get; set; }

        [SerializableProperty]
        public List<DailyContract> Contracts { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetDailyContracts;
        }
    }
}
