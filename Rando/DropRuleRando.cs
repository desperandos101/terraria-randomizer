using System.Configuration;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using System;
using MajorItemRandomizer;
using Terraria.GameContent.ItemDropRules;
using System.Linq;
using LootClass;
using System.Collections.Generic;
using CustomDropRule;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CrateDrop {
    public class ModifyCrates : GlobalItem {
        public static HashSet<int> mundaneCrateIDs = new HashSet<int>() {ItemID.WoodenCrate, ItemID.WoodenCrateHard, ItemID.IronCrate, ItemID.IronCrateHard, ItemID.GoldenCrate, ItemID.GoldenCrateHard};
        private static HashSet<int> biomeCrateIDs = new HashSet<int>() {ItemID.CorruptFishingCrate, ItemID.CorruptFishingCrateHard, ItemID.CrimsonFishingCrate, ItemID.CrimsonFishingCrateHard, ItemID.JungleFishingCrate, ItemID.JungleFishingCrateHard, ItemID.FrozenCrate, ItemID.FrozenCrateHard, ItemID.FloatingIslandFishingCrate, ItemID.FloatingIslandFishingCrateHard, ItemID.OceanCrate, ItemID.OceanCrateHard, ItemID.OasisCrate, ItemID.OasisCrateHard, ItemID.DungeonFishingCrate, ItemID.DungeonFishingCrateHard, ItemID.HallowedFishingCrate, ItemID.HallowedFishingCrateHard, ItemID.LavaCrate, ItemID.LavaCrateHard};
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {   
            if (mundaneCrateIDs.Contains(item.type)) { //This block replaces unique crate items, like the sailfish boots.
                List<IItemDropRule> test = itemLoot.Get();
                if (test[0] is AlwaysAtleastOneSuccessDropRule seqRule) {
                    seqRule.rules[0] = new LootsetDropRule(1);
                    if (item.type == ItemID.WoodenCrate || item.type == ItemID.WoodenCrateHard)
                        seqRule.rules[1] = new LootsetDropRule(1, true);
                }
            } else if (biomeCrateIDs.Contains(item.type)) { //This block replaces biome crate items.
                List<IItemDropRule> test = itemLoot.Get();
                if (test[0] is AlwaysAtleastOneSuccessDropRule seqRule)
                    seqRule.rules[0] = new LootsetDropRule(1, true);
            } else { //This block replaces bag items.
                LootSet mySet = SetManagement.mySet;
                int[] options = mySet.GetInitialRuleOptions(item.type); //Skipping the npc format step, we dont have to do this with boss bags
                if (options.Length != 0) {
                    itemLoot.RemoveWhere(rule => NPCDropRule.CheckRule(rule, options));
                    itemLoot.Add(new LootsetDropRule(1));
                }
            }
        }
    }
    public class NPCDropRule : GlobalNPC {
        public static IItemDropRule[] GetChainedRules(LeadingConditionRule condRule) {
            List<IItemDropRule> ruleList = new List<IItemDropRule>();
            foreach (IItemDropRuleChainAttempt rule in condRule.ChainedRules) {
                IItemDropRule newRule = rule.RuleToChain;
                if (newRule is LeadingConditionRule anotherCondRule) {
                    ruleList.AddRange(GetChainedRules(anotherCondRule));
                } else {
                    ruleList.Add(newRule);
                }
            }
            return ruleList.ToArray();
        }
        public static bool CheckRule(IItemDropRule rule, int[] options) {
            if (rule is DropBasedOnExpertMode expertRule) {
                return RuleHasSetItems(expertRule.ruleForNormalMode, options) || RuleHasSetItems(expertRule.ruleForExpertMode, options);
            } else if (rule is LeadingConditionRule condRule) {
                return GetChainedRules(condRule).Any(p => RuleHasSetItems(p, options));
            } else {
                return RuleHasSetItems(rule, options);
            }
        }
        public static bool RuleHasSetItems(IItemDropRule rule, int[] options) {
            IItemDropRule[] rules;
            if (rule is OneFromRulesRule rulesRule) {
                rules = rulesRule.options;
            } else {
                rules = new IItemDropRule[] {rule};
            }
            return rules.Any(rule => rule is CommonDrop normalDropRule && options.Contains(normalDropRule.itemId) 
            || rule is OneFromOptionsDropRule seqRule && options.Intersect(seqRule.dropIds).Any() 
            || rule is OneFromOptionsNotScaledWithLuckDropRule seqRule2 && options.Intersect(seqRule2.dropIds).Any());
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (new int [] {NPCID.DukeFishron}.Contains(npc.type))
                Console.WriteLine("<LOADED>");
            LootSet mySet = SetManagement.mySet;
            int npcTypeFormatted = npc.IDNPC();
            int[] itemsToRemove = mySet.GetInitialRuleOptions(npcTypeFormatted);
            itemsToRemove = itemsToRemove.Concat(mySet.GetGlobalItems()).ToArray();
            if (itemsToRemove.Length != 0)
            {	
                npcLoot.RemoveWhere(rule => CheckRule(rule, itemsToRemove));
            }
            if (mySet.GetRulePools(npcTypeFormatted).Any())
                npcLoot.Add(new LootsetDropRule(50));
            if (new int [] {NPCID.Plantera}.Contains(npc.type))
                npcLoot.Add(new CommonDrop(ItemID.TempleKey, 1));
            
        }
    }
}