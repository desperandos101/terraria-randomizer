using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ModLoader;

using LootClass;

using MyExtensions;

namespace MajorItemRandomizer
{
    public class EnemyLoot : GlobalNPC {
	
		public void AddToShop(ref Item[] items, int newItem, int index, bool insertingItem = true) {
			int[] newItemSet = ItemReference.GetItemSet(newItem, true);
			if (insertingItem) {
				var itemSlices = items.SplitArray(index);
				var firstSlice = itemSlices.Item1;
				var secondSlice = itemSlices.Item2.OffsetInventory(1, newItemSet.Length);

				for(int i = 0; i < newItemSet.Length; i++) {
					secondSlice[i].SetDefaults(newItemSet[i], false);
				}
				items = firstSlice.Concat(secondSlice).ToArray();
			} else {
				for(int i = 0; i < newItemSet.Length; i++) {
					items[index + i] = new Item(newItemSet[i]);
				}
			}

			
		}
		public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
		{	
			LootSet mySet = SetManagement.mySet;
			for (int j = 0; j < 2; j++) {
				int npcid = (1 - (j * 2)) * npc.type;
				if (mySet.shopSet.ContainsKey(npcid) && mySet.shopSet[npcid].randoEnabled) {
					LootPool pool = mySet.shopSet[npcid];
					int[] poolSet = pool.GetSet();
					List<int> extraItems = pool.randomSet.Skip(pool.initialSet.Length).ToList();
					for (int i = 0; i < items.Length; i++) {
						Item item = items[i];
						
						int newItem;
						bool itemNull = item == null;
						if(!itemNull && pool.initialSet.Contains(item.type)) {
							int index = Array.IndexOf(pool.initialSet, item.type);
							item.type = 0;
							if (index < poolSet.Length) {
								newItem = poolSet[index];
								AddToShop(ref items, newItem, i);
								continue;
							}
						}
						if ((itemNull || item.type == 0) && extraItems.Count() > 0) {
							newItem = extraItems[0];
							extraItems.RemoveAt(0);
							AddToShop(ref items, newItem, i, false);
						} else if (itemNull) {
							break;
						}
						
					}
				}
			}
		}
	}
}