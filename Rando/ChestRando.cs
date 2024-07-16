using System;
using System.Collections.Generic;
using System.Linq;
using static System.Random;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using Terraria.GameContent.Achievements;

using LootClass;
using Terraria.GameContent.ItemDropRules;
using MajorItemRandomizer;
using CustomDropRule;
using System.Linq.Expressions;
using ReLogic.Content;

using MyExtensions;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;
using Terraria.GameContent.Bestiary;
using Config;
using CrateDrop;
using Mono.CompilerServices.SymbolWriter;

using Terraria;
using MajorItemRandomizer.RegionLocking;

namespace MajorItemRandomizer {
    public static class ChestRando {
        public static void RandomizeChests() {
            var chestList = from chest in Main.chest
							where chest != null
							select chest;
			
			foreach (Chest chest in chestList) {
				Tile mainTile = Main.tile[chest.x, chest.y];

				int chestKey = mainTile.IDChest();
				if (chestKey == -1) {
					continue;
				}
				/*
				Main.tile[chest.x, chest.y].ResetToType(TileID.Containers2);
				Main.tile[chest.x+1, chest.y].ResetToType(TileID.Containers2);
				Main.tile[chest.x, chest.y+1].ResetToType(TileID.Containers2);
				Main.tile[chest.x+1, chest.y+1].ResetToType(TileID.Containers2);
				*/

				if (SetManagement.mySet.chestSet.Keys.Contains(chestKey) && SetManagement.mySet.chestSet[chestKey].randoEnabled) {
					int oldItem = chest.item[0].type;
					int newItem = SetManagement.mySet.chestSet[chestKey].GetNext();

					int[] oldItemSet = ItemReference.GetItemSet(oldItem);
					int[] newItemSet = ItemReference.GetItemSet(newItem);
					chest.item = chest.item.OffsetInventory(oldItemSet.Length, newItemSet.Length);

					for(int i = 0; i < newItemSet.Length; i++) {
						chest.item[i].SetDefaults(newItemSet[i], false);
						chest.item[i].stack = newItemSet[i].GetQuant();
					}
				}
			}
        }
    }
}