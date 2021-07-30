using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetSkillsResponse : ApiResponse
    {
        [SerializableProperty]
        public List<int> Skills { get; set; }

        [SerializableProperty]
        public int SkillPoints { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetSkills;
        }
    }
}
