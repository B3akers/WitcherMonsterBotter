using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Logging;

namespace WitcherMonsterBotter.Bot.Farm
{
    public class FarmIngredientsWorker
    {
        private ClientConnection _client;
        public FarmIngredientsWorker(ClientConnection client) { _client = client; }

        public async Task<bool> Farm(Dictionary<int, int> ingredients)
        {
            if (ingredients.Count <= 0)
                return false;

            Logger.Log(Logger.LogType.INFO, "Farming for ingredients...");

            var monsterDropTable = _client.StaticGameData.drop_table_tier_ingredients.Where(x => ingredients.Keys.Contains(x.ingredient_id)).ToList();

            Dictionary<int, List<int>> monstersToFarm = new();

            foreach (var drop in monsterDropTable)
            {
                var dropTableTier = _client.StaticGameData.drop_table_tiers.FirstOrDefault(x => x.id == drop.drop_table_tier_id);

                if (dropTableTier != null)
                {
                    if (!monstersToFarm.TryGetValue(dropTableTier.monster_id, out var listIngredients))
                    {
                        listIngredients = new();
                        monstersToFarm[dropTableTier.monster_id] = listIngredients;
                    }

                    listIngredients.Add(drop.ingredient_id);
                }
            }

            Logger.Log(Logger.LogType.INFO, "Monster farming...");

            FARM_MONSTER:
            if (monstersToFarm.Count > 0)
            {
                var battleResult = await _client.MonsterSlayerWorker.FightWithMonster(monstersToFarm.Keys.ToList());

                if (battleResult == null)
                {
                    Logger.Log(Logger.LogType.WARNING, "We coudn't find proper monster in our area changing location...");
                  
                    var newLocationResult = await _client.LocationWorker.GetLocationForMonsters(monstersToFarm.Keys.ToList());
                    if (newLocationResult.Item1)
                        _client.LocationWorker.UpdateLocation(newLocationResult.Item2, newLocationResult.Item3);

                    return false;
                }

                Dictionary<int, int> ingredientsToRemove = new();

                foreach (var item in battleResult.Loot)
                {
                    if (ingredients.ContainsKey(item))
                    {
                        ingredients[item]--;
                    }
                    else
                    {
                        if (ingredientsToRemove.TryGetValue(item, out int count))
                        {
                            count++;
                        }
                        else
                        {
                            count = 1;
                        }

                        ingredientsToRemove[item] = count;
                    }
                }

                await _client.MonsterSlayerWorker.DropItems(ingredientsToRemove);

                List<int> ingredientsFarmed = new();

                foreach (var item in ingredients)
                {
                    if (item.Value <= 0)
                    {
                        ingredientsFarmed.Add(item.Key);
                    }
                }

                foreach (var item in ingredientsFarmed)
                {
                    ingredients.Remove(item);

                    foreach (var ingredientsList in monstersToFarm.Values)
                    {
                        ingredientsList.Remove(item);
                    }
                }

                List<int> monstersToRemoveFromList = new();

                foreach (var monsterFarmInfo in monstersToFarm)
                {
                    if (monsterFarmInfo.Value.Count <= 0)
                    {
                        monstersToRemoveFromList.Add(monsterFarmInfo.Key);
                    }
                }

                foreach (var removeKey in monstersToRemoveFromList)
                {
                    monstersToFarm.Remove(removeKey);
                }

                goto FARM_MONSTER;
            }

            Logger.Log(Logger.LogType.INFO, "Herb farming...");

            var herbDtopTable = _client.StaticGameData.herb_ingredients.Where(x => ingredients.Keys.Contains(x.ingredient_id)).ToList();
            var herbs = _client.LocationWorker.GetHerbs();

            HERB_FARM:
            if (herbDtopTable.Count > 0)
            {
                var herbsIds = herbDtopTable.Select(x => x.herb_id).ToList();
                var herb = herbs.FirstOrDefault(x => herbsIds.Contains(x.Entity.Type));

                if (herb != null)
                {
                    var resultGather = await _client.Handler.GatherHerb(herb.Entity.InstanceId);
                    if (resultGather.Success)
                    {
                        Dictionary<int, int> herbsToRemove = new();

                        foreach (var item in resultGather.Loot)
                        {
                            if (ingredients.ContainsKey(item))
                            {
                                ingredients[item]--;
                            }
                            else
                            {
                                if (herbsToRemove.TryGetValue(item, out int count))
                                {
                                    count++;
                                }
                                else
                                {
                                    count = 1;
                                }

                                herbsToRemove[item] = count;
                            }
                        }

                        await _client.MonsterSlayerWorker.DropItems(herbsToRemove);

                        List<int> ingredientsToRemove = new();

                        foreach (var ingredient in ingredients)
                        {
                            if (ingredient.Value <= 0)
                            {
                                ingredientsToRemove.Add(ingredient.Key);
                            }
                        }

                        foreach (var key in ingredientsToRemove)
                        {
                            ingredients.Remove(key);
                        }

                        herbDtopTable.RemoveAll(x => !ingredients.Keys.Contains(x.ingredient_id));
                    }

                    herbs.Remove(herb);
                    goto HERB_FARM;
                }
                else
                {
                    Logger.Log(Logger.LogType.WARNING, "We coudn't find proper herb in our area changing location...");
                  
                    var locationResult = await Geo.GeoLocationFinder.FindLocation(true, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 });
                    if (locationResult.Item1)
                        _client.LocationWorker.UpdateLocation(locationResult.Item2, locationResult.Item3);

                    return false;
                }
            }

            Logger.Log(Logger.LogType.SUCCESS, "Items farmed!");

            return true;
        }
    }
}
