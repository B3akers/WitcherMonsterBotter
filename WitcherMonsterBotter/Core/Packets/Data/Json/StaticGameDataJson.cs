using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Data.Json
{
    public class Achievement
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object image_path { get; set; }
        public int contract_id { get; set; }
        public string slug { get; set; }
    }

    public class AreaGoogleName
    {
        public int area_id { get; set; }
        public string google_name { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ArmorToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int power { get; set; }
        public int effect_apply_type_id { get; set; }
    }

    public class Armor
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public string release_version { get; set; }
    }

    public class AutoEquip
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public int item_type_id { get; set; }
        public int attack_strong { get; set; }
        public int attack_steel { get; set; }
        public int attack_silver { get; set; }
        public int attack_fire { get; set; }
        public int attack_kinetic { get; set; }
        public int attack_fast { get; set; }
        public int attack_dimeritium { get; set; }
        public int day { get; set; }
        public int night { get; set; }
        public int weather_normal { get; set; }
        public int weather_rain { get; set; }
        public int weather_fog { get; set; }
        public int family_draconide { get; set; }
        public int family_ogroid { get; set; }
        public int family_hybrid { get; set; }
        public int family_elemental { get; set; }
        public int family_relict { get; set; }
        public int family_specter { get; set; }
        public int family_insectoid { get; set; }
        public int family_vampire { get; set; }
        public int family_cursed { get; set; }
        public int family_necrophage { get; set; }
        public int difficulty1 { get; set; }
        public int difficulty2 { get; set; }
        public int difficulty3 { get; set; }
        public int difficulty4 { get; set; }
        public int difficulty5 { get; set; }
        public int difficulty6 { get; set; }
        public int difficulty7 { get; set; }
        public int difficulty8 { get; set; }
    }

    public class AutoEquipItemsPrice
    {
        public int id { get; set; }
        public int item_type_id { get; set; }
        public int item_id { get; set; }
        public int gold_price { get; set; }
    }

    public class BiomeGoogleId
    {
        public int biome_id { get; set; }
        public int google_id { get; set; }
    }

    public class Biome
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class BombDamageType
    {
        public int bomb_id { get; set; }
        public int damage_type_id { get; set; }
        public int amount { get; set; }
    }

    public class ItemRecipeIngredient
    {
        public int recipe_id { get; set; }
        public int ingredient_id { get; set; }
        public int amount { get; set; }
    }

    public class ItemRecipe
    {
        public int id { get; set; }
        public object name { get; set; }
        public int output { get; set; }
        public int tier_id { get; set; }
        public object image_path { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int crafting_time { get; set; }
    }

    public class Bomb
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public string prefab_path { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int value { get; set; }
        public int radius { get; set; }
        public int explode_style { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
    }

    public class Brewer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public string prefab_path { get; set; }
        public string slug { get; set; }
        public int uses { get; set; }
        public int priority { get; set; }
        public int time_coefficient { get; set; }
    }

    public class Consumable
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int duration { get; set; }
        public int priority { get; set; }
    }

    public class ConsumablesPlayerModifier
    {
        public int consumable_id { get; set; }
        public int player_modifier_id { get; set; }
    }

    public class ContractAction
    {
        public int contract_id { get; set; }
        public int action_type { get; set; }
    }

    public class ContractCombatUsage
    {
        public int contract_id { get; set; }
        public int item_id { get; set; }
    }

    public class ContractCraft
    {
        public int contract_id { get; set; }
        public int item_id { get; set; }
    }

    public class ContractMonster
    {
        public int contract_id { get; set; }
        public int monster_id { get; set; }
    }

    public class ContractQuestNodeOutput
    {
        public int contract_id { get; set; }
        public int quest_node_output_id { get; set; }
    }

    public class ContractType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Contract
    {
        public int id { get; set; }
        public int contract_type_id { get; set; }
        public int value1 { get; set; }
        public int value2 { get; set; }
        public int value3 { get; set; }
    }

    public class CurrentVersion
    {
        public int version { get; set; }
    }

    public class CustomizationHead
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
    }

    public class DailyQuest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public int reward { get; set; }
        public int contract_id { get; set; }
        public string slug { get; set; }
        public int is_active { get; set; }
    }

    public class DamageType
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int power { get; set; }
    }

    public class Difficulty
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int player_attack_count { get; set; }
        public int enemy_attack_count { get; set; }
    }

    public class DropTableTierIngredient
    {
        public int drop_table_tier_id { get; set; }
        public int ingredient_id { get; set; }
    }

    public class DropTableTier
    {
        public int id { get; set; }
        public int monster_id { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
    }

    public class EffectApplyType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class EffectType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Effect
    {
        public int id { get; set; }
        public string name { get; set; }
        public int effect_type_id { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public int order_value { get; set; }
        public int tutorial { get; set; }
        public string starting_datetime_utc { get; set; }
        public string ending_datetime_utc { get; set; }
    }

    public class EventsDailyQuest
    {
        public int id { get; set; }
        public int daily_quest_id { get; set; }
        public int order_value { get; set; }
        public int event_id { get; set; }
    }

    public class GameConfiguration
    {
        public int id { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
    }

    public class HerbIngredient
    {
        public int herb_id { get; set; }
        public int ingredient_id { get; set; }
        public int min_count { get; set; }
        public int max_count { get; set; }
    }

    public class Herb
    {
        public int id { get; set; }
        public string name { get; set; }
        public string prefab_path { get; set; }
        public string icon { get; set; }
        public int base_spawn_weight { get; set; }
        public int biome_id { get; set; }
        public int biome_spawn_modifier { get; set; }
        public int min_respawn_time { get; set; }
        public int max_respawn_time { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
    }

    public class InappPriceShop
    {
        public int inapp_price_id { get; set; }
        public string name { get; set; }
        public string shop_name { get; set; }
        public string shop_price_id { get; set; }
    }

    public class InappPrice
    {
        public int id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string product_type { get; set; }
    }

    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int rarity { get; set; }
    }

    public class ItemHint
    {
        public int item_type_id { get; set; }
        public int item_id { get; set; }
        public string hint { get; set; }
    }

    public class ItemType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class LevelUpBomb
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpBrewer
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpConsumable
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpLure
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpOil
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpPotion
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUpSensesPotion
    {
        public int level_up_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class LevelUp
    {
        public int id { get; set; }
        public int exp_threshold { get; set; }
        public int skill_points { get; set; }
    }

    public class LocalMonstersConfiguration
    {
        public int id { get; set; }
        public int item_type_id { get; set; }
        public int item_id { get; set; }
        public int? monster_id { get; set; }
        public int weight_common { get; set; }
        public int weight_common_skull { get; set; }
        public int weight_rare { get; set; }
        public int weight_legendary { get; set; }
    }

    public class Lure
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public int monster_family_id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
    }

    public class MonsterDescription
    {
        public int id { get; set; }
        public string content { get; set; }
        public int level { get; set; }
        public int threshold { get; set; }
        public int monster_id { get; set; }
    }

    public class MonsterDropRoll
    {
        public int monster_id { get; set; }
        public int min_rolls { get; set; }
        public int max_rolls { get; set; }
    }

    public class MonsterFamily
    {
        public int id { get; set; }
        public string name { get; set; }
        public string small_image { get; set; }
        public string big_image { get; set; }
        public string small_light_image { get; set; }
        public string slug { get; set; }
    }

    public class MonsterSpawnArea
    {
        public int monster_id { get; set; }
        public int area_id { get; set; }
        public int spawn_modifier { get; set; }
    }

    public class MonsterSpawnBiome
    {
        public int monster_id { get; set; }
        public int biome_id { get; set; }
        public int spawn_modifier { get; set; }
    }

    public class MonsterSpawnDayNight
    {
        public int monster_id { get; set; }
        public int spawn_modifier { get; set; }
        public int time_of_day_id { get; set; }
    }

    public class MonsterSpawnFullMoon
    {
        public int monster_id { get; set; }
        public int is_full_moon { get; set; }
        public int spawn_modifier { get; set; }
    }

    public class MonsterSpawnTime
    {
        public int monster_id { get; set; }
        public int seconds_of_day_from { get; set; }
        public int seconds_of_day_to { get; set; }
        public int spawn_modifier { get; set; }
    }

    public class MonsterSpawnWeatherCondition
    {
        public int monster_id { get; set; }
        public int weather_condition_id { get; set; }
        public int spawn_modifier { get; set; }
    }

    public class MonsterVulnerability
    {
        public int monster_id { get; set; }
        public int damage_type_id { get; set; }
    }

    public class Monster
    {
        public int id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string image { get; set; }
        public int family_id { get; set; }
        public int base_spawn_weight { get; set; }
        public int rarity { get; set; }
        public int difficulty { get; set; }
        public string trophy { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int active { get; set; }
        public string release_version { get; set; }
        public string season { get; set; }
    }

    public class Nest
    {
        public int id { get; set; }
        public int first_minion_difficulty { get; set; }
        public int second_minion_difficulty { get; set; }
        public int boss_difficulty { get; set; }
        public int available_by_default { get; set; }
        public int base_spawn_weight { get; set; }
        public int nest_clearing_exp { get; set; }
        public int nest_clearing_gold { get; set; }
        public int minimal_player_level { get; set; }
        public int maximal_player_level { get; set; }
        public int first_minion_rarity { get; set; }
        public int second_minion_rarity { get; set; }
        public int boss_rarity { get; set; }
    }

    public class OilToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int power { get; set; }
        public int effect_apply_type_id { get; set; }
    }

    public class Oil
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int exp_matching { get; set; }
    }

    public class PacksAlchemist
    {
        public int id { get; set; }
        public int ingredient_id { get; set; }
        public int amount { get; set; }
        public int weight { get; set; }
    }

    public class PacksHerbalist
    {
        public int id { get; set; }
        public int ingredient_id { get; set; }
        public int min_roll { get; set; }
        public int max_roll { get; set; }
        public int weight { get; set; }
    }

    public class PacksLootConfiguration
    {
        public int id { get; set; }
        public int chance { get; set; }
        public int packs_count { get; set; }
    }

    public class PacksMerchant
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public int item_type_id { get; set; }
        public int amount { get; set; }
        public int weight { get; set; }
    }

    public class PacksType
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int chance { get; set; }
        public int priority { get; set; }
    }

    public class PlayerModifierToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int effect_apply_type_id { get; set; }
        public int power { get; set; }
    }

    public class PlayerModifier
    {
        public int id { get; set; }
        public string slug { get; set; }
    }

    public class PlayerStartingArmor
    {
        public int id { get; set; }
        public int is_equipped { get; set; }
    }

    public class PlayerStartingBombRecipe
    {
        public int id { get; set; }
    }

    public class PlayerStartingBomb
    {
        public int id { get; set; }
        public int amount { get; set; }
    }

    public class PlayerStartingBrewer
    {
        public int id { get; set; }
    }

    public class PlayerStartingIngredient
    {
        public int id { get; set; }
        public int amount { get; set; }
    }

    public class PlayerStartingOilRecipe
    {
        public int id { get; set; }
    }

    public class PlayerStartingOil
    {
        public int id { get; set; }
        public int amount { get; set; }
    }

    public class PlayerStartingPotionRecipe
    {
        public int id { get; set; }
    }

    public class PlayerStartingPotion
    {
        public int id { get; set; }
        public int amount { get; set; }
    }

    public class PlayerStartingSensesPotionRecipe
    {
        public int id { get; set; }
    }

    public class PlayerStartingSensesPotion
    {
        public int id { get; set; }
        public int amount { get; set; }
    }

    public class PlayerStartingSkill
    {
        public int id { get; set; }
    }

    public class PlayerStartingSword
    {
        public int id { get; set; }
        public int is_equipped { get; set; }
    }

    public class PotionToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int effect_apply_type_id { get; set; }
        public int power { get; set; }
    }

    public class Potion
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int auto_equip_priority { get; set; }
    }

    public class QuestDisplayMode
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class QuestEdge
    {
        public int from_quest_id { get; set; }
        public int to_quest_id { get; set; }
    }

    public class QuestFact
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class QuestNodeEdge
    {
        public int from_quest_node_output_id { get; set; }
        public int to_quest_node_id { get; set; }
    }

    public class QuestNodeOutputArmor
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
    }

    public class QuestNodeOutputBestiaryEntry
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class QuestNodeOutputBomb
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class QuestNodeOutputIngredient
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class QuestNodeOutputOil
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class QuestNodeOutputPotion
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
    }

    public class QuestNodeOutputSkill
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
    }

    public class QuestNodeOutputSword
    {
        public int quest_node_output_id { get; set; }
        public int item_id { get; set; }
    }

    public class QuestNodeOutput
    {
        public int id { get; set; }
        public int quest_node_id { get; set; }
        public string name { get; set; }
        public int experience { get; set; }
        public int gold { get; set; }
        public int endpoint { get; set; }
        public int active { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class QuestNode
    {
        public int id { get; set; }
        public int quest_id { get; set; }
        public string name { get; set; }
        public int placement_type { get; set; }
        public string placement_directions { get; set; }
        public int? placement_directions_origin_quest_node_id { get; set; }
        public int placement_story_point_id { get; set; }
        public string quest_affair_name { get; set; }
        public string quest_prefab_name { get; set; }
        public string map_sound { get; set; }
        public int quest_display_mode_id { get; set; }
        public int repeatable { get; set; }
        public string activation_criteria { get; set; }
        public int active { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int ttl { get; set; }
    }

    public class QuestStoryPoint
    {
        public int id { get; set; }
        public string name { get; set; }
        public int season_id { get; set; }
    }

    public class Quest
    {
        public int id { get; set; }
        public string name { get; set; }
        public int season_id { get; set; }
        public string journal_log { get; set; }
        public string activation_criteria { get; set; }
        public int active { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Season
    {
        public int id { get; set; }
        public string name { get; set; }
        public int active { get; set; }
    }

    public class SensesPotionEffect
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class SensesPotion
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_path { get; set; }
        public int effect_id { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
    }

    public class ShopBundleDay
    {
        public string name { get; set; }
        public int shop_bundle_id { get; set; }
        public int day_of_month { get; set; }
    }

    public class ShopBundleItem
    {
        public int id { get; set; }
        public int item_type_id { get; set; }
        public int item_id { get; set; }
        public int amount { get; set; }
        public int shop_bundle_id { get; set; }
    }

    public class ShopBundle
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public int dev { get; set; }
        public int permanent { get; set; }
        public int one_time { get; set; }
        public int? inapp_price_id { get; set; }
        public object layout_group_name { get; set; }
        public string layout_type { get; set; }
        public int layout_priority { get; set; }
        public string layout_underlay { get; set; }
        public int gold_price { get; set; }
        public int active { get; set; }
        public int discount { get; set; }
    }

    public class ShopBundlesLayoutGroupNameCategory
    {
        public int id { get; set; }
        public string layout_group_name_main_category { get; set; }
        public string layout_group_name_sub_category { get; set; }
        public int shop_bundle_id { get; set; }
    }

    public class SkillRequirement
    {
        public int skill_id { get; set; }
        public int required_skill_id { get; set; }
    }

    public class SkillToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int power { get; set; }
        public int effect_apply_type_id { get; set; }
    }

    public class Skill
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int cost { get; set; }
        public int required_level { get; set; }
        public int? parent_id { get; set; }
    }

    public class SummoningScroll
    {
        public int id { get; set; }
        public string slug { get; set; }
        public int duration { get; set; }
        public int priority { get; set; }
    }

    public class SummoningScrollsPlayerModifier
    {
        public int summoning_scroll_id { get; set; }
        public int player_modifier_id { get; set; }
    }

    public class SwordToEffect
    {
        public int item_id { get; set; }
        public int effect_id { get; set; }
        public int power { get; set; }
        public int effect_apply_type_id { get; set; }
    }

    public class SwordType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Sword
    {
        public int id { get; set; }
        public int sword_type { get; set; }
        public string slug { get; set; }
        public int priority { get; set; }
        public int auto_equip_priority { get; set; }
        public string release_version { get; set; }
    }

    public class TimesOfDay
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class VersionLog
    {
        public int data_version { get; set; }
        public string client_version { get; set; }
    }

    public class WeatherConditionCode
    {
        public int weather_condition_id { get; set; }
        public int code { get; set; }
    }

    public class WeatherCondition
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class WeatherIconCode
    {
        public int weather_icon_id { get; set; }
        public int code { get; set; }
    }

    public class WeatherIcon
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class WeeklyQuestsReward
    {
        public int id { get; set; }
        public int weekly_quest_tier_id { get; set; }
        public int item_type_id { get; set; }
        public int amount { get; set; }
        public int gold { get; set; }
        public int item_id { get; set; }
    }

    public class WeeklyQuestsTier
    {
        public int tier { get; set; }
        public int chance { get; set; }
    }

    public class StaticGameDataJson
    {
        public List<Achievement> achievements { get; set; }
        public List<AreaGoogleName> area_google_names { get; set; }
        public List<Area> areas { get; set; }
        public List<ArmorToEffect> armor_to_effect { get; set; }
        public List<Armor> armors { get; set; }
        public List<AutoEquip> auto_equip { get; set; }
        public List<AutoEquipItemsPrice> auto_equip_items_prices { get; set; }
        public List<BiomeGoogleId> biome_google_ids { get; set; }
        public List<Biome> biomes { get; set; }
        public List<BombDamageType> bomb_damage_types { get; set; }
        public List<ItemRecipeIngredient> bomb_recipe_ingredients { get; set; }
        public List<ItemRecipe> bomb_recipes { get; set; }
        public List<Bomb> bombs { get; set; }
        public List<Brewer> brewers { get; set; }
        public List<Consumable> consumables { get; set; }
        public List<ConsumablesPlayerModifier> consumables_player_modifiers { get; set; }
        public List<ContractAction> contract_actions { get; set; }
        public List<ContractCombatUsage> contract_combat_usages { get; set; }
        public List<ContractCraft> contract_crafts { get; set; }
        public List<ContractMonster> contract_monsters { get; set; }
        public List<ContractQuestNodeOutput> contract_quest_node_outputs { get; set; }
        public List<object> contract_quests { get; set; }
        public List<object> contract_skills { get; set; }
        public List<ContractType> contract_types { get; set; }
        public List<Contract> contracts { get; set; }
        public List<CurrentVersion> current_versions { get; set; }
        public List<CustomizationHead> customization_heads { get; set; }
        public List<DailyQuest> daily_quests { get; set; }
        public List<DamageType> damage_types { get; set; }
        public List<Difficulty> difficulties { get; set; }
        public List<DropTableTierIngredient> drop_table_tier_ingredients { get; set; }
        public List<DropTableTier> drop_table_tiers { get; set; }
        public List<EffectApplyType> effect_apply_types { get; set; }
        public List<EffectType> effect_types { get; set; }
        public List<Effect> effects { get; set; }
        public List<Event> events { get; set; }
        public List<EventsDailyQuest> events_daily_quests { get; set; }
        public List<GameConfiguration> game_configuration { get; set; }
        public List<HerbIngredient> herb_ingredients { get; set; }
        public List<Herb> herbs { get; set; }
        public List<InappPriceShop> inapp_price_shops { get; set; }
        public List<InappPrice> inapp_prices { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<ItemHint> item_hints { get; set; }
        public List<ItemType> item_types { get; set; }
        public List<object> level_up_bomb_recipes { get; set; }
        public List<LevelUpBomb> level_up_bombs { get; set; }
        public List<LevelUpBrewer> level_up_brewers { get; set; }
        public List<LevelUpConsumable> level_up_consumables { get; set; }
        public List<object> level_up_ingredients { get; set; }
        public List<object> level_up_lure_recipes { get; set; }
        public List<LevelUpLure> level_up_lures { get; set; }
        public List<object> level_up_oil_recipes { get; set; }
        public List<LevelUpOil> level_up_oils { get; set; }
        public List<object> level_up_potion_recipes { get; set; }
        public List<LevelUpPotion> level_up_potions { get; set; }
        public List<object> level_up_senses_potion_recipes { get; set; }
        public List<LevelUpSensesPotion> level_up_senses_potions { get; set; }
        public List<object> level_up_summoning_scrolls { get; set; }
        public List<LevelUp> level_ups { get; set; }
        public List<LocalMonstersConfiguration> local_monsters_configuration { get; set; }
        public List<ItemRecipeIngredient> lure_recipe_ingredients { get; set; }
        public List<ItemRecipe> lure_recipes { get; set; }
        public List<Lure> lures { get; set; }
        public List<MonsterDescription> monster_descriptions { get; set; }
        public List<MonsterDropRoll> monster_drop_rolls { get; set; }
        public List<MonsterFamily> monster_families { get; set; }
        public List<object> monster_immunities { get; set; }
        public List<MonsterSpawnArea> monster_spawn_areas { get; set; }
        public List<MonsterSpawnBiome> monster_spawn_biomes { get; set; }
        public List<MonsterSpawnDayNight> monster_spawn_day_nights { get; set; }
        public List<MonsterSpawnFullMoon> monster_spawn_full_moons { get; set; }
        public List<MonsterSpawnTime> monster_spawn_times { get; set; }
        public List<MonsterSpawnWeatherCondition> monster_spawn_weather_conditions { get; set; }
        public List<MonsterVulnerability> monster_vulnerabilities { get; set; }
        public List<Monster> monsters { get; set; }
        public List<Nest> nests { get; set; }
        public List<ItemRecipeIngredient> oil_recipe_ingredients { get; set; }
        public List<ItemRecipe> oil_recipes { get; set; }
        public List<OilToEffect> oil_to_effect { get; set; }
        public List<Oil> oils { get; set; }
        public List<PacksAlchemist> packs_alchemists { get; set; }
        public List<PacksHerbalist> packs_herbalists { get; set; }
        public List<PacksLootConfiguration> packs_loot_configurations { get; set; }
        public List<PacksMerchant> packs_merchants { get; set; }
        public List<PacksType> packs_types { get; set; }
        public List<PlayerModifierToEffect> player_modifier_to_effect { get; set; }
        public List<PlayerModifier> player_modifiers { get; set; }
        public List<PlayerStartingArmor> player_starting_armors { get; set; }
        public List<PlayerStartingBombRecipe> player_starting_bomb_recipes { get; set; }
        public List<PlayerStartingBomb> player_starting_bombs { get; set; }
        public List<PlayerStartingBrewer> player_starting_brewers { get; set; }
        public List<PlayerStartingIngredient> player_starting_ingredients { get; set; }
        public List<object> player_starting_lure_recipes { get; set; }
        public List<object> player_starting_lures { get; set; }
        public List<PlayerStartingOilRecipe> player_starting_oil_recipes { get; set; }
        public List<PlayerStartingOil> player_starting_oils { get; set; }
        public List<PlayerStartingPotionRecipe> player_starting_potion_recipes { get; set; }
        public List<PlayerStartingPotion> player_starting_potions { get; set; }
        public List<PlayerStartingSensesPotionRecipe> player_starting_senses_potion_recipes { get; set; }
        public List<PlayerStartingSensesPotion> player_starting_senses_potions { get; set; }
        public List<PlayerStartingSkill> player_starting_skills { get; set; }
        public List<PlayerStartingSword> player_starting_swords { get; set; }
        public List<ItemRecipeIngredient> potion_recipe_ingredients { get; set; }
        public List<ItemRecipe> potion_recipes { get; set; }
        public List<PotionToEffect> potion_to_effect { get; set; }
        public List<Potion> potions { get; set; }
        public List<object> quest_affairs { get; set; }
        public List<QuestDisplayMode> quest_display_modes { get; set; }
        public List<QuestEdge> quest_edges { get; set; }
        public List<QuestFact> quest_facts { get; set; }
        public List<QuestNodeEdge> quest_node_edges { get; set; }
        public List<QuestNodeOutputArmor> quest_node_output_armors { get; set; }
        public List<QuestNodeOutputBestiaryEntry> quest_node_output_bestiary_entries { get; set; }
        public List<QuestNodeOutputBomb> quest_node_output_bombs { get; set; }
        public List<QuestNodeOutputIngredient> quest_node_output_ingredients { get; set; }
        public List<object> quest_node_output_lures { get; set; }
        public List<QuestNodeOutputOil> quest_node_output_oils { get; set; }
        public List<QuestNodeOutputPotion> quest_node_output_potions { get; set; }
        public List<object> quest_node_output_senses_potions { get; set; }
        public List<QuestNodeOutputSkill> quest_node_output_skills { get; set; }
        public List<QuestNodeOutputSword> quest_node_output_swords { get; set; }
        public List<QuestNodeOutput> quest_node_outputs { get; set; }
        public List<QuestNode> quest_nodes { get; set; }
        public List<object> quest_prefabs { get; set; }
        public List<QuestStoryPoint> quest_story_points { get; set; }
        public List<Quest> quests { get; set; }
        public List<Season> seasons { get; set; }
        public List<SensesPotionEffect> senses_potion_effects { get; set; }
        public List<ItemRecipeIngredient> senses_potion_recipe_ingredients { get; set; }
        public List<ItemRecipe> senses_potion_recipes { get; set; }
        public List<SensesPotion> senses_potions { get; set; }
        public List<ShopBundleDay> shop_bundle_days { get; set; }
        public List<ShopBundleItem> shop_bundle_items { get; set; }
        public List<ShopBundle> shop_bundles { get; set; }
        public List<ShopBundlesLayoutGroupNameCategory> shop_bundles_layout_group_name_categories { get; set; }
        public List<SkillRequirement> skill_requirements { get; set; }
        public List<SkillToEffect> skill_to_effect { get; set; }
        public List<Skill> skills { get; set; }
        public List<SummoningScroll> summoning_scrolls { get; set; }
        public List<SummoningScrollsPlayerModifier> summoning_scrolls_player_modifiers { get; set; }
        public List<SwordToEffect> sword_to_effect { get; set; }
        public List<SwordType> sword_types { get; set; }
        public List<Sword> swords { get; set; }
        public List<TimesOfDay> times_of_day { get; set; }
        public List<VersionLog> version_logs { get; set; }
        public List<object> vulnerabilities { get; set; }
        public List<WeatherConditionCode> weather_condition_codes { get; set; }
        public List<WeatherCondition> weather_conditions { get; set; }
        public List<WeatherIconCode> weather_icon_codes { get; set; }
        public List<WeatherIcon> weather_icons { get; set; }
        public List<WeeklyQuestsReward> weekly_quests_rewards { get; set; }
        public List<WeeklyQuestsTier> weekly_quests_tiers { get; set; }
    }

}
