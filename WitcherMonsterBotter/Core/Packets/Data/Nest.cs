using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Nest
    {
        [SerializableProperty]
        public long InstanceId { get; set; }

        [SerializableProperty]
        public int BossType { get; set; }

        [SerializableProperty]
        public int NestState { get; set; }
    }

    public enum NestState : byte
    {
        Default = 1,
        DefaultClear,
        Lured,
        LuredClear,
        NotAllowed
    }
}
