using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class CombatEndResponse : ApiResponse
    {
        [SerializableProperty]
        public List<int> Loot { get; set; }
        [SerializableProperty]
        public int BaseExp { get; set; }
        [SerializableProperty]
        public int ComboExp { get; set; }
        [SerializableProperty]
        public int OilExp { get; set; }
        [SerializableProperty]
        public int TimeExp { get; set; }
        [SerializableProperty]
        public int PerfectParryExp { get; set; }
        [SerializableProperty]
        public int FirstTimeExp { get; set; }
        [SerializableProperty]
        public int BoostedExp { get; set; }
        [SerializableProperty]
        public int PackType { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.CombatEnd;
        }
    }
}
