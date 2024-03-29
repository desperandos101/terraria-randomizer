using System;
using System.Collections.Generic;
using System.Linq;
using static System.Random;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent.ItemDropRules;

using LootClass;
using ItemSwapper;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography;

namespace CustomDropRule {
    public class LootsetDropRule : IItemDropRule {
        public int npcID;
        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }
        public LootsetDropRule(int myNPC) {
            npcID = myNPC;
            ChainedRules = new List<IItemDropRuleChainAttempt>();
        }
        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) {
            int npcIDformatted = ItemReference.IDNPC(info.npc.type);
            LootSet mySet = ChestSpawn.mySet;

            List<int> options = new List<int>();
            NPCLootPool[] myPools = mySet.GetNPCPools(npcIDformatted);
            
            foreach (LootPool pool in myPools) {
                options = options.Concat(pool.randomSet).ToList();
            }
            int itemId = options[info.rng.Next(options.Count)];
            int[] itemSet = ItemReference.GetItemSet(itemId);
            foreach (int id in itemSet) {
                CommonCode.DropItem(info, id, ItemReference.GetQuant(id));
            }
            ItemDropAttemptResult result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.Success;
            return result;
        }
        public bool CanDrop(DropAttemptInfo info) => true;
        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
{
	Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
}
    }
}