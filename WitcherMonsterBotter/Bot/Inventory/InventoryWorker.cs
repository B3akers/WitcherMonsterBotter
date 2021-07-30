using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;

namespace WitcherMonsterBotter.Bot.Inventory
{
    public class InventoryWorker
    {
        private ClientConnection _client;
        public InventoryWorker(ClientConnection client) { _client = client; }

        private Core.Packets.Api.Response.GetInventoryResponse _inventoryResponse = null;

        public async Task Update()
        {
            _inventoryResponse = await _client.Handler.GetInventory();
        }

        public Core.Packets.Api.Response.GetInventoryResponse GetInventory()
        {
            return _inventoryResponse;
        }
    }
}
