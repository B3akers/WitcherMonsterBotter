using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;

namespace WitcherMonsterBotter.Bot.Character
{
    public class CharacterWorker
    {
        private ClientConnection _client;
        public CharacterWorker(ClientConnection client) { _client = client; }

        private Core.Packets.Api.Response.GetPlayerInfoResponse _playerInfoResponse;
        private int _level;

        public int Level
        {
            get
            {
                return _level;
            }
        }

        public Core.Packets.Api.Response.GetPlayerInfoResponse GetPlayerInfo()
        {
            return _playerInfoResponse;
        }

        public async Task Update()
        {
            _playerInfoResponse = await _client.Handler.GetPlayerInfo();
            _level = _client.StaticGameData.level_ups.Where(x => _playerInfoResponse.Exp >= x.exp_threshold).Max(x => x.id);
        }
    }
}
