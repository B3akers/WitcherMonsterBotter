using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class ExpiringPlayerModifier
    {
        [SerializableProperty]
        public int Id { get; set; }

        [SerializableProperty]
        public int StartTimestamp { get; set; }

        [SerializableProperty]
        public int ExpireTimestamp { get; set; }
    }
}
