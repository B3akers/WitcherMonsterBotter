using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetPlayerInfoResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public string Name { get; set; }

        [SerializableProperty]
        public int Gold { get; set; }

        [SerializableProperty]
        public int Exp { get; set; }

        [SerializableProperty]
        public int Head { get; set; }

        [SerializableProperty]
        public bool TutorialFinished { get; set; }

        [SerializableProperty]
        public byte Gender { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetPlayerInfo;
        }
    }
}
