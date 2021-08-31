using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Packets.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WitcherMonsterBotter.Core.Logging;

namespace WitcherMonsterBotter.Bot.Geo
{
    public class LocationWorker
    {
        private Random _random;
        private ClientConnection _client;

        public LocationWorker(ClientConnection client) { _client = client; _random = new(); }

        private Core.Packets.Api.Response.GetLocationsByCellResponse _locationsResponse;
        private bool _updateLocation = false;
        private double _lat;
        private double _lng;

        public async Task UpdateCells(double lat, double lng)
        {
            _locationsResponse = await _client.Handler.GetLocationsByCell(Core.Geo.Location.GetPlayerCells(lat, lng));
        }

        public async Task Update()
        {
            if (!_updateLocation)
                return;

            await UpdateCells(_lat, _lng);

            _updateLocation = false;
        }

        public void UpdateLocation(double lat, double lng)
        {
            _updateLocation = true;
            _lat = lat;
            _lng = lng;
        }

        public async Task<Tuple<bool, double, double>> GetLocationForMonsters(List<int> monsters)
        {
            var moonPhase = Math.Abs(MoonPhase.GetMoonPhase(DateTime.UtcNow));

            foreach (var monster in monsters)
            {
                bool moonRequired = false;
                HashSet<int> disabledSpawnHours = new HashSet<int>();

                foreach (var monsterSpawn in _client.StaticGameData.monster_spawn_full_moons.Where(x => x.monster_id == monster))
                {
                    if (monsterSpawn.is_full_moon == 0 && monsterSpawn.spawn_modifier < 0)
                    {
                        moonRequired = true;
                        break;
                    }
                }

                if (moonRequired && moonPhase < 96.0)
                    continue;

                foreach (var monsterSpawn in _client.StaticGameData.monster_spawn_times.Where(x => x.monster_id == monster))
                {
                    if (monsterSpawn.spawn_modifier < 0)
                    {
                        var start = monsterSpawn.seconds_of_day_from / 3600;
                        var end = monsterSpawn.seconds_of_day_to / 3600;
                        for (; start < end; start++)
                        {
                            disabledSpawnHours.Add(start);
                        }
                    }
                }

                var allowedHours = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 }.Where(x => !disabledSpawnHours.Contains(x)).ToArray();
                var locationResult = await GeoLocationFinder.FindLocation(true, allowedHours);

                if (locationResult.Item1)
                    return locationResult;
            }

            return new Tuple<bool, double, double>(false, 0.0, 0.0);
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
