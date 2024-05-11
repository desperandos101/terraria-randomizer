using Humanizer;
using LootClass;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Sources;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static System.Random;

namespace MajorItemRandomizer {
    public static class ItemReference {
        private static Random rnd = new Random();
        #region Private Datasets
        private static readonly (int, int[])[] NPCIDSets = {
            (3, new int[] {132, 186, 187, 188, 189, 200, 223, 161, 254, 255, 52, 53, 536, 319, 320, 321, 332, 436, 431, 432, 433, 434, 435, 331, 430, 590}),
            (1, new int[] {302, 333, 334, 335, 336}),
            (494, new int[] {495}),
            (496, new int[] {497}),
            (498, new int[] {499, 500, 501, 502, 503, 504, 505, 506}),
            (42, new int[] {-16, -17, 231, -56, -57, 232, -58, -59, 233, -60, -61, 234, -62, -63, 235, -64, -65}),
            (176, new int[] {-18, -19, -20, -21}),
            (21, new int[] {449, -46, -47, 201, -48, -49, 202, -50, -51, 203, -52, -53, 322, 323, 324, 635}),
            (3187, new int[] {3188, 3189}),
            (580, new int[] {508}),
            (581, new int[] {509}),
            (195, new int[] {196}),
            (NPCID.RustyArmoredBonesAxe, new int[] {NPCID.RustyArmoredBonesFlail, NPCID.RustyArmoredBonesSword, NPCID.RustyArmoredBonesSwordNoArmor}),
            (NPCID.BlueArmoredBones, new int[] {NPCID.BlueArmoredBonesMace, NPCID.BlueArmoredBonesNoPants, NPCID.BlueArmoredBonesSword}),
            (NPCID.HellArmoredBones, new int[] {NPCID.HellArmoredBonesMace, NPCID.HellArmoredBonesSpikeShield, NPCID.HellArmoredBonesSword}),
            (NPCID.DiabolistRed, new int[] {NPCID.DiabolistWhite}),
            (NPCID.Necromancer, new int[] {NPCID.NecromancerArmored}),
            (NPCID.RaggedCaster, new int[] {NPCID.RaggedCasterOpenCoat})
            };
        public static int[] eowIDs = new int[] {13, 14, 15};
        
                private static readonly Dictionary<int, int[]> ItemSetDict = new Dictionary<int, int[]> {
            {6, new int[] {-11, -12}},
            {930, new int[] {931}},
            {848, new int[] {866}},
            {960, new int[] {961, 962}},
            {954, new int[] {81, 77}},
            {955, new int[] {83, 79}},
            {956, new int[] {957, 958}},
            {410, new int[] {411}},
            {ItemID.AnglerHat, new int[] {ItemID.AnglerVest, ItemID.AnglerPants}},
            {ItemID.SeashellHairpin, new int[] {ItemID.MermaidAdornment, ItemID.MermaidTail}},
            {ItemID.FishCostumeMask, new int[] {ItemID.FishCostumeShirt, ItemID.FishCostumeFinskirt}},
            {ItemID.NinjaHood, new int[] {ItemID.NinjaShirt, ItemID.NinjaPants}},
            {ItemID.GladiatorHelmet, new int[] {ItemID.GladiatorBreastplate, ItemID.GladiatorLeggings}},
            {4982, new int[] {4983, 4984}},
            {ItemID.Stynger, new int[] {ItemID.StyngerBolt}},
            {ItemID.StakeLauncher, new int[] {ItemID.Stake}}
        };
        private static readonly Dictionary<int, int> ChestDict = new Dictionary<int, int> {
            {21, 0},
            {467, 52}
        };
        private static readonly Dictionary<(int, int), int> WallOverride = new Dictionary<(int, int), int> {
            {(1, 187), 69} //Pyramid Chests
        };
        private static readonly Dictionary<int, (int, int)> ItemQuantDict = new Dictionary<int, (int, int)> {
            {931, (25, 50)},
            {ItemID.StyngerBolt, (60, 99)},
            {ItemID.Stake, (30, 60)}
        };

        public static int GetQuant(this int itemID) {
            if (ItemQuantDict.ContainsKey(itemID)) {
                    int lowerBound = ItemQuantDict[itemID].Item1;
                    int upperBound = ItemQuantDict[itemID].Item2;
                    return rnd.Next(lowerBound, upperBound);
                }
            return 1;
        }
        private static readonly HashSet<int> Sellables = new HashSet<int> {
            931, 97
        };
        #endregion
        public static int[] GetItemSet(int itemID, bool stripShop = false)
        {
            if (itemID == 0)
                return new int[0];
            int[] ItemSetsNew = new int[] {itemID};
            
            if (ItemSetDict.ContainsKey(itemID)) {
                int[] ItemSets = ItemSetDict[itemID];
                return ItemSetsNew.Concat(ItemSets).ToArray();
            }
            if(stripShop) {
                ItemSetsNew = StripSellables(ItemSetsNew);
            }
            return ItemSetsNew;
        }
        public static int[] AddExtrasToItemSet(int[] itemSet) {
            int[] newSet = new int[] {};
            foreach (int item in itemSet) {
                newSet = newSet.Concat(GetItemSet(item)).ToArray();
            }
            return newSet;
        }
        public static int[] StripSellables(int[] itemSet) => (from item in itemSet where !Sellables.Contains(item) select item).ToArray();
        public static Item[] OffsetInventory(this Item[] inventory, int oldSetLength, int newSetLength) {
            if (oldSetLength != newSetLength) {
                (int, int)[] inventoryTypes = (from item in inventory where item != null select (item.type, item.stack)).ToArray();
                int offset = newSetLength - oldSetLength;
                for (int i = 0; i < inventoryTypes.Length; i++)
                {
                    if (i + offset < 0 || i + offset > inventory.Length - 1)
                    {
                        continue;
                    }
                    
                    if (inventory[i + offset] == null) {
                        inventory[i + offset] = new Item();
                    }
                    inventory[i + offset].SetDefaults(inventoryTypes[i].Item1, false);
                    inventory[i + offset].stack = inventoryTypes[i].Item2;
                }
            }
            return inventory;
					
        }
        public static int IDChest(this Tile chest) {
            int chestTileID = chest.TileType;
			int chestType = chest.TileFrameX;
			int chestWall = chest.WallType;
            if (!ChestDict.ContainsKey(chestTileID)) {
                return -1;
            }
            if (chestType % 36 != 0) {
                throw new Exception($"TileFrameX, {chestType}, is not divisible by 36.");
            }
            int ChestID = chestType / 36 + ChestDict[chestTileID];
            if (WallOverride.Keys.Contains((ChestID, chestWall))) {
                return WallOverride[(ChestID, chestWall)];
            }
            return ChestID;
        }

        
        public static int IDNPC(this NPC npc) {
            int id = npc.type;
            foreach((int, int[]) idSet in NPCIDSets) {
                if(idSet.Item2.Contains(id)) {
                    return idSet.Item1;
                }
            }
            return id;
        }
        public static int IDNPC(this int id) {
            foreach((int, int[]) idSet in NPCIDSets) {
                if(idSet.Item2.Contains(id)) {
                    return idSet.Item1;
                }
            }
            return id;
        }
    }
}