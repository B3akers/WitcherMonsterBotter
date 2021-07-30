using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Api;

namespace WitcherMonsterBotter.Core.Packets.Data
{
    public class Brewer
    {
        [SerializableProperty]
        public long InstanceId { get; set; }

        [SerializableProperty]
        public int Type { get; set; }

        [SerializableProperty]
        public int UsesLeft { get; set; }

        [SerializableProperty]
        public int WorkingRecipe { get; set; }

        [SerializableProperty]
        public int FinishTime { get; set; }

        public const int INFINITE_BREWER = 1;
        public const int BIG_BREWER = 3;
        public const int SMALL_BREWER = 2;
    }
}
