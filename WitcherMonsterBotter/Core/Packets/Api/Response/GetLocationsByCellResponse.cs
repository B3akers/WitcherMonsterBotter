using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetLocationsByCellResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public Dictionary<ulong, List<Location>> LocationMap { get; set; }

        [SerializableProperty]
        public List<Placement<Monster>> MonsterPlacements { get; set; }

        [SerializableProperty]
        public List<Placement<Herb>> HerbPlacements { get; set; }

        [SerializableProperty]
        public List<Placement<QuestNodeInstance>> QuestNodeInstancePlacements { get; set; }

        [SerializableProperty]
        public List<Placement<Nest>> NestPlacements { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetLocationsByCell;
        }
    }
}
