using Humanizer;
using MajorItemRandomizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using MyExtensions;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Sources;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using UtfUnknown.Core.Probers;
using static System.Random;
namespace LootClass {
    public class LootSet : TagSerializable
    {
        static Random rnd = new();
        #region Pool Sets
        public Dictionary<int, LootPool> chestSet = new Dictionary<int, LootPool>();
        public Dictionary<int, LootPool> dropRuleSet = new Dictionary<int, LootPool>();
        public Dictionary<int, LootPool> shopSet = new Dictionary<int, LootPool>();
        public HashSet<LootPool> fishSet = new HashSet<LootPool>();
        public HashSet<LootPool> smashSet = new HashSet<LootPool>();
        public List<LootPool> questSet = new List<LootPool>();
        public Dictionary<int, List<int>> globalSet = new Dictionary<int, List<int>>() {
            {0, new List<int>()},
            {1, new List<int>()}
        };
        #endregion
        #region Add to Pool Methods
        public void AddChestPool(int theRegion, int[] chestIDs, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            foreach (int chestID in chestIDs) {
                chestSet[chestID] = newPool;
            }
        }
        public void AddRulePool(int theRegion, int[] npcIDs, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            foreach (int npcID in npcIDs) {
                dropRuleSet[npcID] = newPool;
            }
            
        }
        public void AddShopPool(int theRegion, int npcID, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            shopSet[npcID] = newPool;
        }
        public void AddFishPool(int theRegion, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            fishSet.Add(newPool);
        }
        public void AddBiomeCratePool(int[] crateIDs, LootPool pool) {
            foreach (int crateID in crateIDs)
                chestSet[crateID] = pool;
        }
        public void AddSmashPool(int theRegion, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            smashSet.Add(newPool);
        }
        public void AddQuestPool(int theRegion, int[] itemList, int slots = 0, int scoops = 0) {
            LootPool newPool = new(theRegion, itemList, slots, scoops);
            questSet.Add(newPool);
        }
        public void AddGlobalItems(int theRegion, int[] items) => globalSet[theRegion].AddRange(items);
        #endregion
        #region Rule Collection Methods
        public LootPool GetRulePool(int npcID) => dropRuleSet.Keys.Contains(npcID) ? dropRuleSet[npcID] : null;
        public int[] GetInitialRuleOptions(int npcID) {
            LootPool pool = GetRulePool(npcID);
            List<int> options = new List<int>();
            foreach(int itemType in pool.initialSet)
                options.AddRange(ItemReference.GetItemSet(itemType));
            return options.ToArray();
        }
        public LootPool GetFishPool(int itemID) => fishSet.FirstOrDefault(pool => pool.initialSet.Contains(itemID));
        public LootPool GetOrbPool(int itemID) => smashSet.FirstOrDefault(pool => pool.initialSet.Contains(itemID));
        public int[] GetGlobalItems() => globalSet[0].Union(globalSet[1]).ToArray();
        #endregion
        public void DisablePool(LootPool pool) => pool.randoEnabled = false;
        public void DisablePools(IEnumerable<LootPool> poolSet, Func<LootPool, bool> filter = null) {
            if (poolSet is Dictionary<int, LootPool> dict)
                poolSet = dict.Values;
            foreach (LootPool pool in poolSet)
                if (filter is null || filter(pool)) {
                    pool.randoEnabled = false;
                }
        }
        public void AddPoolItems(List<int> items, IEnumerable<LootPool> pools) {
            foreach(LootPool pool in pools)
                items.AddRange(pool.initialSet);
        }
        #region Randomization
        public (int, int[])[] Limiter = new (int, int[])[] {
            (1, new int[] {ItemID.DarkHorseSaddle, ItemID.PaintedHorseSaddle, ItemID.MajesticHorseSaddle}),
            (1, new int[] {ItemID.DD2BallistraTowerT1Popper, ItemID.DD2ExplosiveTrapT1Popper, ItemID.DD2FlameburstTowerT1Popper, ItemID.DD2LightningAuraT1Popper}),
            (1, new int[] {ItemID.DD2BallistraTowerT2Popper, ItemID.DD2ExplosiveTrapT2Popper, ItemID.DD2FlameburstTowerT2Popper, ItemID.DD2LightningAuraT2Popper}),
            (1, new int[] {ItemID.DD2BallistraTowerT3Popper, ItemID.DD2ExplosiveTrapT3Popper, ItemID.DD2FlameburstTowerT3Popper, ItemID.DD2LightningAuraT3Popper}),
            (1, new int[] {ItemID.SquireShield, ItemID.MonkBelt, ItemID.ApprenticeScarf, ItemID.HuntressBuckler}),
            (1, new int[] {ItemID.RedsWings, ItemID.DTownsWings, ItemID.WillsWings, ItemID.CrownosWings, ItemID.CenxsWings, 3228, ItemID.Yoraiz0rWings, ItemID.JimsWings, ItemID.SkiphsWings, ItemID.LokisWings, ItemID.ArkhalisWings, ItemID.LeinforsWings, ItemID.GhostarsWings, ItemID.SafemanWings, ItemID.FoodBarbarianWings, ItemID.GroxTheGreatWings}),
            (2, new int[] {ItemID.FinWings, ItemID.IceFeather, ItemID.Jetpack, ItemID.LeafWings, ItemID.BrokenBatWing, ItemID.TatteredBeeWing, ItemID.ButterflyDust, ItemID.FireFeather, ItemID.BoneFeather, ItemID.MothronWings, ItemID.FestiveWings, ItemID.SpookyTwig, ItemID.BlackFairyDust, ItemID.SteampunkWings, ItemID.BetsyWings, ItemID.RainbowWings, ItemID.FishronWings})
        };
        public IEnumerable<LootPool> GetValidPools(IEnumerable<LootPool> pools, int region) => from pool in pools where pool.region == region && pool.randoEnabled select pool;
        public void SortAndFill(IEnumerable<LootPool> pools, List<int> allItems, ref int allItemCount, List<LootPool> scooperPools, int defaultSetSize = 0) {
            foreach (LootPool pool in pools) {
                if (pool.scoops == 0) {
                    allItemCount -= pool.Fill(allItems, defaultSetSize);
                } else {
                    scooperPools.Add(pool);
                }
            }
        }
        public void Randomize() {
            for (int i = 0; i < 2; i++) {
                List<int> allItems = new();
                HashSet<LootPool> validChests = (from key in chestSet.Keys where key < 100 && chestSet[key].randoEnabled && chestSet[key].region == i select chestSet[key]).ToHashSet(); //pretty dumb exclusion, but this should prevent specifically biome crates from reappearing
                
                IEnumerable<LootPool> validRules = GetValidPools(dropRuleSet.Values, i);
                IEnumerable<LootPool> validShops = GetValidPools(shopSet.Values, i);
                IEnumerable<LootPool> validFishes = GetValidPools(fishSet, i);
                IEnumerable<LootPool> validSmashes = GetValidPools(smashSet, i);
                IEnumerable<LootPool> validQuests = GetValidPools(questSet, i);

                AddPoolItems(allItems, validChests);
                AddPoolItems(allItems, validRules);
                AddPoolItems(allItems, validShops);
                AddPoolItems(allItems, validFishes);
                AddPoolItems(allItems, validSmashes);
                AddPoolItems(allItems, validQuests);
                allItems.AddRange(globalSet[i]);

                foreach ((int, int[]) limitSet in Limiter) {
                    int limit = limitSet.Item1;
                    int[] removableItems = limitSet.Item2;
                    int[] itemsToRemove = removableItems.GetRandomSubset(removableItems.Length - limit);
                    allItems.RemoveAll(i => itemsToRemove.Contains(i));
                }

                if (allItems.ContainsDuplicates())
                    throw new Exception("CONTAINS DUPLICATES");

                int totalItemCount = allItems.Count();
                List<LootPool> scooperPools = new List<LootPool>() {}; 

                SortAndFill(validChests, allItems, ref totalItemCount, scooperPools);
                SortAndFill(validRules, allItems, ref totalItemCount, scooperPools, 1 - i);
                SortAndFill(validShops, allItems, ref totalItemCount, scooperPools);
                SortAndFill(validFishes, allItems, ref totalItemCount, scooperPools);
                SortAndFill(validSmashes, allItems, ref totalItemCount, scooperPools);
                SortAndFill(validQuests, allItems, ref totalItemCount, scooperPools);

                int totalScoops = 0;
                foreach (LootPool pool in scooperPools) {
                    totalScoops += pool.scoops;
                }

                scooperPools.Sort((p1, p2) => p1.scoops - p2.scoops);

                if (totalScoops != 0) {
                    int chestSlots = totalItemCount / totalScoops;
                    int chestSlotRemainder = totalItemCount % totalScoops;
                    foreach (LootPool pool in scooperPools) {
                        if (chestSlotRemainder > 0) { // Initially, we want to avoid giving the remainders to pools with larger scoop counts.
                            totalItemCount -= pool.Fill(allItems, chestSlots * pool.scoops + 1);
                            chestSlotRemainder--;
                        } else {
                            totalItemCount -= pool.Fill(allItems, chestSlots * pool.scoops);
                        }
                    }
                }
                if (totalItemCount != 0) {
                    throw new Exception("We fucked up! Back to the lab again");
                }
            }
            for (int i = 0; i > 10000; i++)
                if (GetRulePool(i) is null) //i forget what this was for
                    throw new Exception("fuck");
            Console.WriteLine(this);

        }
        private string GetSetNames(LootPool pool) {
            string theText = "";
            foreach (int item in pool.GetSet())
                theText += $"{Lang.GetItemName(item)}, ";
            return theText;
        }
        public override string ToString()
        {
            string theText = "";
            foreach (int key in chestSet.Keys) {
                theText += $"CHEST {key}: ";
                theText += $"{GetSetNames(chestSet[key])}\n";
            }
            foreach (int key in dropRuleSet.Keys) { //This will loop over enemies that lead to the same pool. Too bad. So sad.
                LootPool pool = dropRuleSet[key];
                theText += $"NPCS ";
                theText += $"{Lang.GetNPCName(key)} ";
                theText += ": ";
                theText += $"{GetSetNames(pool)}\n";
            }
            foreach (int key in shopSet.Keys) {
                theText += $"SHOP {Lang.GetNPCName(key)}: ";
                theText += $"{GetSetNames(shopSet[key])}\n";
            }
            foreach (LootPool pool in fishSet) {
                theText += $"FISH ";
                foreach (int fish in pool.initialSet)
                    theText += $"{Lang.GetItemName(fish)} ";
                theText += ": ";
                theText += $"{GetSetNames(pool)}\n";
            }
            foreach (LootPool pool in smashSet) {
                theText += $"SMASHABLES ";
                foreach (int item in pool.initialSet)
                    theText += $"{Lang.GetItemName(item)} ";
                theText += ": ";
                theText += $"{GetSetNames(pool)}\n";
            }
            foreach (LootPool pool in questSet) {
                theText += $"QUEST FISH: ";
                theText += $"{GetSetNames(pool)}\n";
            }
            return theText;
        }
        #endregion
        #region TagSerialize Methods
        public static readonly Func<TagCompound, LootSet> DESERIALIZER = Load;
        public TagCompound SerializeData()
        {
            return new TagCompound 
            {
                ["chestSetKeys"] = chestSet.Keys.ToList(),
                ["chestSetValues"] = chestSet.Values.ToList(),
                ["dropRuleSetKeys"] = dropRuleSet.Keys.ToList(),
                ["dropRuleSetValues"] = dropRuleSet.Values.ToList(),
                ["shopSetKeys"] = shopSet.Keys.ToList(),
                ["shopSetValues"] = shopSet.Values.ToList(),
                ["fishSet"] = fishSet.ToList(),
                ["smashSet"] = smashSet.ToList(),
                ["questSet"] = questSet,
            };
        }
        public static LootSet Load(TagCompound tag)
        {
            var lootset = new LootSet();
            List<int> chestKeys = tag.Get<List<int>>("chestSetKeys");
            List<LootPool> chestValues = tag.Get<List<LootPool>>("chestSetValues");
            for (int i = 0; i < chestKeys.Count; i++)
            {
                lootset.chestSet[chestKeys[i]] = chestValues[i];
            }
            List<int> dropKeys = tag.Get<List<int>>("dropRuleSetKeys");
            List<LootPool> dropValues = tag.Get<List<LootPool>>("dropRuleSetValues");
            for (int i = 0; i < dropKeys.Count; i++) {
                lootset.dropRuleSet[dropKeys[i]] = dropValues[i];
            }
            List<int> shopKeys = tag.Get<List<int>>("shopSetKeys");
            List<LootPool> shopValues = tag.Get<List<LootPool>>("shopSetValues");
            for (int i = 0; i < shopKeys.Count; i++)
            {
                lootset.shopSet[shopKeys[i]] = shopValues[i];
            }
            lootset.fishSet = tag.Get<List<LootPool>>("fishSet").ToHashSet();
            lootset.smashSet = tag.Get<List<LootPool>>("smashSet").ToHashSet();
            lootset.questSet = tag.Get<List<LootPool>>("questSet");
            return lootset;
        }
        #endregion
    }
    public class LootPool : TagSerializable {
            protected int counter;
            public int[] initialSet;
            public int[] randomSet;
            public bool randoEnabled = true;
            #region World Gen Data
            //This data is not saved or loaded after randomization is done, as this info is only needed during generation
            public int region;
            public bool fillRandom = true;
            public int scoops; //ONLY FOR USE WITH LEFTOVER SLOTS! Use slots to set size otherwise
            #endregion
            public LootPool(int theRegion, int[] itemList, int slots, int theScoops = 0) {
                region = theRegion;
                initialSet = itemList;
                if (slots == -1) {
                    randomSet = new int[0];
                    fillRandom = false;
                } else {
                    randomSet = new int[slots];
                }
                scoops = theScoops;
            }
            public int Fill(List<int> items, int length = 0) {
                if (fillRandom) {
                    if (randomSet.Length != 0) {
                    length = randomSet.Length;
                    } else if (length == 0) {
                        length = initialSet.Length;
                    }
                    randomSet = items.GetRandomSubset(length, true);
                    return length;
                } else {
                    return 0;
                }
            }
            public virtual int GetNext() {
                int item;
                if (randoEnabled && randomSet is not null) {
                    item = randomSet[counter];
                    counter = (counter + 1)%randomSet.Length;
                } else { //if the pool is not enabled, we just default to what is normally there
                    item = initialSet[counter];
                    counter = (counter + 1)%initialSet.Length;
                }
                return item;
            }
            public int[] GetSet() => randoEnabled ? randomSet : initialSet;
            #region TagSerialize Methods
            public static readonly Func<TagCompound, LootPool> DESERIALIZER = Load;
            public virtual TagCompound SerializeData()
            {
                return new TagCompound
                {
                    ["counter"] = counter,
                    ["initialSet"] = initialSet,
                    ["randomSet"] = randomSet,
                    ["randoEnabled"] = randoEnabled,
                    ["region"] = region,
                };
            }

            public static LootPool Load(TagCompound tag)
            {
                var chestPool = new LootPool(tag.GetInt("region"), tag.GetIntArray("initialSet"), 1);
                chestPool.counter = tag.GetInt("counter");
                chestPool.randomSet = tag.GetIntArray("randomSet");
                chestPool.randoEnabled = tag.GetBool("randoEnabled");
                return chestPool;
            }
            #endregion
    }
        
}
