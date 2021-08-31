using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Packets.Data;

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

        public int GetItemsSize()
        {
            return _inventoryResponse.IngredientMap.Values.Sum()
                + _inventoryResponse.BombMap.Values.Sum()
                 + _inventoryResponse.PotionMap.Values.Sum()
                  + _inventoryResponse.OilMap.Values.Sum()
                   + _inventoryResponse.LureMap.Values.Sum()
                    + _inventoryResponse.SensesPotionMap.Values.Sum()
                     + _inventoryResponse.ConsumablesMap.Values.Sum()
                     + _inventoryResponse.FriendsPacksMap.Values.Sum()
                     + _inventoryResponse.SummoningScrollsMap.Values.Sum();
        }

        public async Task ClenupInventory()
        {
            float percent = (float)GetItemsSize() / _inventoryResponse.BagSize;

            if (percent < 0.75f)
                return;

            foreach (var pack in _inventoryResponse.FriendsPacksMap)
                await _client.Handler.DropFriendPack(pack.Key, pack.Value);

            foreach (var bomb in _inventoryResponse.BombMap)
                if (bomb.Value > 15)
                    await _client.Handler.DropBomb(bomb.Key, bomb.Value - 15);

            foreach (var oil in _inventoryResponse.OilMap)
                if (oil.Value > 10)
                    await _client.Handler.DropOil(oil.Key, oil.Value - 10);

            foreach (var potion in _inventoryResponse.PotionMap)
                if (potion.Value > 10)
                    await _client.Handler.DropPotion(potion.Key, potion.Value - 10);
        }

        public Dictionary<int, int> GetItemsByItemType(int type)
        {
            switch (type)
            {
                case ItemTypeId.BOMBS:
                    return _inventoryResponse.BombMap;
                case ItemTypeId.LURES:
                    return _inventoryResponse.LureMap;
                case ItemTypeId.OILS:
                    return _inventoryResponse.OilMap;
                case ItemTypeId.POTIONS:
                    return _inventoryResponse.PotionMap;
                case ItemTypeId.SENSES_POTIONS:
                    return _inventoryResponse.SensesPotionMap;
            }

            return null;
        }
    }
}
