using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api
{
    public class ApiMessage
    {
        public struct TypeId
        {
            public const int REQUEST = 1;
            public const int RESPONSE = 2;
        }

        public byte Type;
        public long[] Received;

        public TypeMessage Message;
    }

    public class TypeMessage
    {
        public enum Method
        {
            GetLocations = 2,
            GetPlayerInfo = 3,
            CraftItem = 4,
            GetInventory = 5,
            GetKnownRecipes = 6,
            GetKilledMonsters = 7,
            CombatEnd = 8,
            GetEquipment = 9,
            PrepareToCombat = 10,
            EquipArmor = 11,
            EquipSteelSword = 12,
            EquipSilverSword = 13,
            GetMonsterInstance = 14,
            GetNestInstance = 15,
            EndNestMonsterCombat = 16,
            EndNestBossCombat = 17,
            UseLure = 18,
            GatherHerb = 19,
            GetDailyContracts = 20,
            AddDailyContract = 21,
            ReshuffleDailyContract = 22,
            RemoveDailyContract = 23,
            GetAchievements = 24,
            AchievementReceived = 25,
            DailyContractCompleted = 26,
            DistanceTraveled = 27,
            SetCustomizationHead = 28,
            SetName = 29,
            SetTutorialFinished = 30,
            LevelUp = 31,
            BuyPotion = 32,
            BuyBomb = 33,
            BuyOil = 34,
            BuyLure = 35,
            BuyArmor = 36,
            BuySword = 37,
            AddGold = 38,
            ThrowBomb = 39,
            GetLocationsByCell = 40,
            EncounterMonster = 41,
            UseSenses = 42,
            GetSensedMonsters = 43,
            GetCraftingQueue = 44,
            CancelCrafting = 45,
            SetGender = 46,
            DropPotion = 47,
            DropBomb = 48,
            DropOil = 49,
            DropLure = 50,
            DropSensesPotion = 51,
            EncounterNest = 52,
            EndNestCombat = 53,
            SpawnMonster = 54,
            EquipSword = 55,
            GetKilledMonsterInstances = 56,
            EndBehaviourGraph = 57,
            GetFacts = 58,
            GetAllFacts = 59,
            GetActiveQuestNodeInstances = 60,
            GetCurrentObjective = 61,
            SetCurrentObjective = 62,
            GetSkills = 63,
            AcquireSkill = 64,
            ClaimMonsterKnowledgeReward = 65,
            Log = 66,
            GetWeather = 67,
            ClaimRecipe = 68,
            GetBrewers = 69,
            GetFinishedSeasonQuests = 70,
            DropIngredients = 71,
            TrackQuest = 72,
            BuyItem = 73,
            DropItem = 74,
            BuyShopBundle = 75,
            BuyInAppBundle = 76,
            RelocateQuest = 77,
            SetFacts = 78,
            GetDailyShopBundles = 79,
            GetTransactionStatus = 80,
            ClaimDailyQuest = 81,
            AddExp = 82,
            GetOneTimeShopBundles = 83,
            AddExpiringEffect = 84,
            GetExpiringEffects = 85,
            AddSpecifiedContract = 86,
            GetMonsterInstances = 87,
            LoadCells = 88,
            UseOilPotions = 89,
            AddPlayerModifier = 90,
            GetPlayerModifiers = 91,
            RemovePlayerModifier = 92,
            AddSkillPoints = 93,
            GetWeeklyContractProgress = 94,
            ClaimWeeklyQuest = 95,
            AddWeeklyStamps = 96,
            FinishCrafting = 97,
            BuyAutoEquipItems = 98,
            UseConsumable = 99,
            DropConsumable = 100,
            CheckInApp = 101,
            GetFriends = 102,
            AddFriend = 103,
            AcceptFriendInvitation = 104,
            RejectFriendInvitation = 105,
            SendPack = 106,
            OpenPack = 107,
            DeleteFriend = 108,
            DropFriendPack = 109,
            GetFriendsNotifications = 110,
            SummonLocalMonsters = 111,
            GetSummonedMonsters = 112,
            EncounterSummonedMonster = 113,
            CombatEndSummonedMonster = 114,
            GetInitialPlayerData = 115,
            RespawnNest = 116,
            GetLastSummoningSkillUsageTime = 117,
            DropSummoningScroll = 118,
            ResolveRewards = 119
        }

        public long Id;
        public int MethodId;
        public byte[] Data;
    };
}
