using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class SummonedMonsterEntity
    {
        [SerializableProperty]
        public int MonsterId { get; set; }

        [SerializableProperty]
        public long InstanceId { get; set; }

        [SerializableProperty]
        public byte Alive { get; set; }
    }
}
