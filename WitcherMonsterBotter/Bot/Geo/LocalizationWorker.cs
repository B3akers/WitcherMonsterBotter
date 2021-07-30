using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Packets.Data;

namespace WitcherMonsterBotter.Bot.Geo
{
    public class LocalizationWorker
    {
        private Random _random;
        private ClientConnection _client;

        public LocalizationWorker(ClientConnection client) { _client = client; _random = new(); }

        private Core.Packets.Api.Response.GetLocationsByCellResponse _locationsResponse;

        private double _lat, _lng;

        public bool RequestLocationUpdate { get; set; }

        private async Task UpdatePrivate()
        {
            _locationsResponse = await _client.Handler.GetLocationsByCell(Core.Geo.Localization.GetPlayerCells(_lat, _lng));
        }

        public async Task Update(double lat, double lng)
        {
            _lat = lat;
            _lng = lng;

            RequestLocationUpdate = false;

            await UpdatePrivate();
        }

        public async Task Update()
        {
            if (!RequestLocationUpdate)
                return;

            //Better logic
            //
            if (_random.Next(0, 2) == 1)
            {
                _lat += _random.NextDouble();
            }
            else
            {
                _lng += _random.NextDouble();
            }

            await UpdatePrivate();

            RequestLocationUpdate = false;
        }

        public List<Placement<Monster>> GetMonsters()
        {
            return _locationsResponse.MonsterPlacements;
        }

        public List<Placement<Nest>> GetNests()
        {
            return _locationsResponse.NestPlacements;
        }

        public List<Placement<Herb>> GetHerbs()
        {
            return _locationsResponse.HerbPlacements;
        }

    }
}
