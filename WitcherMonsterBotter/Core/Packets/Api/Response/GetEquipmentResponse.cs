using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetEquipmentResponse : ApiResponse
    {
        [SerializableProperty]
        public List<int> Swords { get; set; }
        [SerializableProperty]
        public List<int> Armors { get; set; }
        [SerializableProperty]
        public int EquippedArmor { get; set; }
        [SerializableProperty]
        public int EquippedSword { get; set; }
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetEquipment;
        }
    }
}
