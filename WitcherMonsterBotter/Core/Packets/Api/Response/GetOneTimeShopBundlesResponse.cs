using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class GetOneTimeShopBundlesResponse : ApiResponse
    {
        [SerializableProperty]
        public bool Success { get; set; }

        [SerializableProperty]
        public List<int> OneTimeShopBundles { get; set; }

        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.GetOneTimeShopBundles;
        }
    }
}
