using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitcherMonsterBotter.Core.Connection;
using WitcherMonsterBotter.Core.Logging;
using WitcherMonsterBotter.Core.Packets.Data;
using WitcherMonsterBotter.Core.Packets.Data.Json;

namespace WitcherMonsterBotter.Bot.Brewer
{
    public class BrewerWorker
    {
        private ClientConnection _client;
        public BrewerWorker(ClientConnection client) { _client = client; }

        private Core.Packets.Api.Response.GetKnownRecipesResponse _knownRecipesResponse;
        private Core.Packets.Api.Response.GetBrewersResponse _brewersResponse;

        public async Task UpdateBrewers()
        {
            _brewersResponse = await _client.Handler.GetBrewers();
        }

        public async Task Update()
        {
            _knownRecipesResponse = await _client.Handler.GetKnownRecipes();
            await UpdateBrewers();
        }

        public async Task<bool> PrepareIngredients(Dictionary<int, int> ingredients)
        {
            var inventory = _client.InventoryWorker.GetInventory();

            foreach (var ingredient in ingredients)
            {
                if (inventory.IngredientMap.TryGetValue(ingredient.Key, out int ammount))
                {
                    ingredients[ingredient.Key] -= ammount;
                }
            }

            List<int> removeFromFarm = new();
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Value <= 0)
                    removeFromFarm.Add(ingredient.Key);
            }

            foreach (var key in removeFromFarm)
            {
                ingredients.Remove(key);
            }

            if (ingredients.Count > 0)
            {
                return (await _client.FarmIngredientsWorker.Farm(ingredients));
            }

            return true;
        }

        public async Task ClaimReadyBrewers()
        {
            Logger.Log(Logger.LogType.INFO, "Checking brewers...");

            var timeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (var brewer in _brewersResponse.Brewers)
            {
                if (brewer.WorkingRecipe != -1 && timeNow > brewer.FinishTime)
                {
                    Logger.Log(Logger.LogType.INFO, $"Claming brewer {brewer.InstanceId} finished {timeNow - brewer.FinishTime}s ago");

                    var result = await _client.Handler.ClaimRecipe(brewer.InstanceId);

                    if (result.Success)
                    {
                        brewer.WorkingRecipe = -1;

                        Logger.Log(Logger.LogType.SUCCESS, $"We claimed item from brewer!");
                    }
                    else
                    {
                        Logger.Log(Logger.LogType.WARNING, $"We coudn't claimed from brewer!");
                    }
                }
            }
        }

        public int CountActiveBrewersForRecipt(int recipt)
        {
            return _brewersResponse.Brewers.Count(x => x.WorkingRecipe == recipt);
        }

        public List<long> GetEmptyBrewers()
        {
            List<long> empty = new();

            foreach (var brewer in _brewersResponse.Brewers)
            {
                if ((brewer.UsesLeft == -1 || brewer.UsesLeft > 0) && brewer.WorkingRecipe == -1)
                    empty.Add(brewer.InstanceId);
            }

            return empty;
        }

        private List<ItemRecipe> GetRecipesListByType(int itemType)
        {
            switch (itemType)
            {
                case ItemTypeId.OILS:
                    return _client.StaticGameData.oil_recipes;
                case ItemTypeId.BOMBS:
                    return _client.StaticGameData.bomb_recipes;
                case ItemTypeId.LURES:
                    return _client.StaticGameData.lure_recipes;
                case ItemTypeId.POTIONS:
                    return _client.StaticGameData.potion_recipes;
                case ItemTypeId.SENSES_POTIONS:
                    return _client.StaticGameData.senses_potion_recipes;
            }
            return null;
        }

        private List<ItemRecipeIngredient> GetIngredientsListByType(int itemType)
        {
            switch (itemType)
            {
                case ItemTypeId.OILS:
                    return _client.StaticGameData.oil_recipe_ingredients;
                case ItemTypeId.BOMBS:
                    return _client.StaticGameData.bomb_recipe_ingredients;
                case ItemTypeId.LURES:
                    return _client.StaticGameData.lure_recipe_ingredients;
                case ItemTypeId.POTIONS:
                    return _client.StaticGameData.potion_recipe_ingredients;
                case ItemTypeId.SENSES_POTIONS:
                    return _client.StaticGameData.senses_potion_recipe_ingredients;
            }
            return null;
        }

        public async Task<bool> CreateItem(int itemType, int type = -1, int count = 1)
        {
            var emptyBrewers = GetEmptyBrewers();
            if (emptyBrewers.Count <= 0)
                return false;

            var reciptItems = _knownRecipesResponse.KnownRecipes[itemType];
            var recipesList = GetRecipesListByType(itemType);

            ItemRecipe itemRecipe = null;

            if (type == -1)
            {
                itemRecipe = recipesList.Where(x => reciptItems.Contains(x.id)).OrderBy(x => x.crafting_time).FirstOrDefault();
            }
            else
            {
                if (!reciptItems.Contains(type))
                {
                    Logger.Log(Logger.LogType.WARNING, $"We wanted to create itemm that we don't have recipt {type}!");
                    return false;
                }

                itemRecipe = recipesList.FirstOrDefault(x => x.id == type);
            }

            if (itemRecipe == null)
            {
                Logger.Log(Logger.LogType.WARNING, $"We coudn't find proper recipes for item {type}!");
                return false;
            }

            var itemIngredients = GetIngredientsListByType(itemType).Where(x => x.recipe_id == itemRecipe.id).ToList();
            var currentActiveCount = CountActiveBrewersForRecipt(itemRecipe.id);

            count -= currentActiveCount;

            if (count <= 0)
                return true;

            Dictionary<int, int> craftIngredients = new();

            foreach (var ingredient in itemIngredients)
            {
                craftIngredients.Add(ingredient.ingredient_id, ingredient.amount * count);
            }

            if (!(await PrepareIngredients(craftIngredients)))
                return false;

            foreach (var empty in emptyBrewers)
            {
                var craftResult = await _client.Handler.CraftItem(empty, itemRecipe.id, itemType);

                if (craftResult.Success)
                {
                    Logger.Log(Logger.LogType.SUCCESS, $"Successfully cracted item {itemRecipe.id}!");
                }
            }

            return true;
        }
    }
}
