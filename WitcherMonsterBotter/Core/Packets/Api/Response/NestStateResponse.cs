using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    [SerializableOnlySuccess]
    public abstract class NestStateResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Result { get; set; }
        [SerializableProperty]
        public long NestInstanceId { get; set; }
        [SerializableProperty]
        public int WonNestCombats { get; set; }
        [SerializableProperty]
        public int DailyNestLimit { get; set; }
        [SerializableProperty]
        public List<int> Monsters { get; set; }
        [SerializableProperty]
        public byte State { get; set; }
        [SerializableProperty]
        public int Gold { get; set; }
        [SerializableProperty]
        public int Exp { get; set; }
        [SerializableProperty]
        public int Iteration { get; set; }
    }
}
