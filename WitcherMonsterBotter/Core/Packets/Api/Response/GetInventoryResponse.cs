using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetInventoryResponse : ApiResponse
    {
        [SerializableProperty]
        public Dictionary<int, int> IngredientMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> BombMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> PotionMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> OilMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> LureMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> SensesPotionMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> ConsumablesMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> FriendsPacksMap { get; set; }
        [SerializableProperty]
        public Dictionary<int, int> SummoningScrollsMap { get; set; }
        [SerializableProperty]
        public int BagSize { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetInventory;
        }
    }
}
