using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    [SerializableOnlySuccess]
    public class EndNestCombatResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public List<int> Loot { get; set; }

        [SerializableProperty]
        public int Reward { get; set; }

        [SerializableProperty]
        public int EntireRarityMonsterExp { get; set; }

        [SerializableProperty]
        public int EntireFirstTimeSlayedMonstersExp { get; set; }

        [SerializableProperty]
        public int PerfectAttacksExp { get; set; }

        [SerializableProperty]
        public int ProperOilUsageExp { get; set; }

        [SerializableProperty]
        public int DurationExp { get; set; }

        [SerializableProperty]
        public int PerfectParriesExp { get; set; }

        [SerializableProperty]
        public int NestClearingExp { get; set; }

        [SerializableProperty]
        public int BoostedExp { get; set; }

        [SerializableProperty]
        public int Unknown { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.EndNestCombat;
        }
    }
}
