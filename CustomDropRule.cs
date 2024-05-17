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
using MajorItemRandomizer;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography;
using System.IO.Pipelines;
using MyExtensions;

namespace CustomDropRule {

    public class LootsetDropRule : IItemDropRule {
        public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }
        public int denominator;
        public bool biomeCrate;
        private bool isNotEaterSegment(DropAttemptInfo info) => info.npc is null || !ItemReference.eowIDs.Contains(info.npc.type) || info.npc.boss;
        public LootPool[] Pools(DropAttemptInfo info) {
            int id = info.npc is null ? info.item : info.npc.type;
            return biomeCrate ? new LootPool[] {SetManagement.mySet.chestSet[id]} : SetManagement.mySet.GetRulePools(id.IDNPC());
        }
        public int[] Options(DropAttemptInfo info) {
            LootPool[] pools = Pools(info);
            
            List<int> options = new List<int>();
            foreach (LootPool pool in pools) {
                options.AddRange(pool.GetSet());
            }
            return options.ToArray();
        }
        public LootsetDropRule(int myDenominator, bool isBiomeCrate = false) {
            
            ChainedRules = new List<IItemDropRuleChainAttempt>();
            biomeCrate = isBiomeCrate;
            denominator = myDenominator;
        }
        private static int[] bossNPCs = new int[] {
            NPCID.EyeofCthulhu,
            13,
            14,
            15,
            NPCID.BrainofCthulhu,
            NPCID.KingSlime,
            NPCID.Deerclops,
            NPCID.QueenBee,
            NPCID.SkeletronHead,
            NPCID.DD2DarkMageT1,
            NPCID.GoblinShark,
            NPCID.BloodEelHead,
            NPCID.BloodNautilus,
            NPCID.QueenSlimeBoss,
            NPCID.TheDestroyer,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.SkeletronPrime,
            NPCID.Plantera,
            NPCID.Golem,
            NPCID.HallowBoss,
            NPCID.DukeFishron
        };
        private static int[] alwaysDropNPCS = new int[] {
            NPCID.DyeTrader,
            NPCID.Stylist,
            NPCID.Mechanic,
            NPCID.Painter,
            NPCID.DD2Bartender,
            NPCID.TaxCollector,
            NPCID.Princess,
            NPCID.Mimic,
            NPCID.IceMimic,
            NPCID.BigMimicHallow,
            NPCID.BigMimicCorruption,
            NPCID.BigMimicCrimson,
            NPCID.GoblinSummoner,
            NPCID.PirateShip,
            NPCID.MartianSaucer,
            NPCID.MourningWood,
            NPCID.Pumpking,
            NPCID.Everscream,
            NPCID.SantaNK1,
            NPCID.IceQueen
        };
        private static int[] bossBags = new int[] {
            ItemID.KingSlimeBossBag,
            ItemID.EyeOfCthulhuBossBag,
            ItemID.EaterOfWorldsBossBag,
            ItemID.BrainOfCthulhuBossBag,
            ItemID.SkeletronBossBag,
            ItemID.QueenBeeBossBag,
            ItemID.DeerclopsBossBag,
            ItemID.BossBagDarkMage,
            ItemID.BossBagOgre,
            ItemID.BossBagBetsy,
            ItemID.QueenSlimeBossBag,
            ItemID.DestroyerBossBag,
            ItemID.TwinsBossBag,
            ItemID.SkeletronPrimeBossBag,
            ItemID.PlanteraBossBag,
            ItemID.GolemBossBag,
            ItemID.FairyQueenBossBag,
            ItemID.FishronBossBag};
        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) {
            ItemDropAttemptResult result;
            bool isBoss = info.npc is not null && bossNPCs.Contains(info.npc.IDNPC());
            bool alwaysDrops = info.npc is not null && alwaysDropNPCS.Contains(info.npc.IDNPC());
            if (info.player.RollLuck(denominator) < 1 && isNotEaterSegment(info) || isBoss || alwaysDrops) {
                int[] options = Options(info);
                int minDrop = 1;
                if (isBoss || (info.npc is null && bossBags.Contains(info.item)))
                    minDrop = -1;
                int[] newItemIDs = options.GetRandomSubset(minDrop);
                foreach(int itemID in newItemIDs) {
                    int[] itemSet = ItemReference.GetItemSet(itemID);
                    foreach (int id in itemSet) {
                        CommonCode.DropItem(info, id, id.GetQuant());
                    }
                }

                result = default(ItemDropAttemptResult);
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }
            result = default(ItemDropAttemptResult);
            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
            
        }
        public bool CanDrop(DropAttemptInfo info) {
            if (!isNotEaterSegment(info))
                return false;
            if (info.npc is not null && info.IsExpertMode && info.npc.boss)
                return false;
            if (info.npc is not null && info.npc.type == NPCID.Spazmatism) {
                return !NPC.AnyNPCs(NPCID.Retinazer);
            } else if (info.npc is not null && info.npc.type == NPCID.Retinazer) {
                return !NPC.AnyNPCs(NPCID.Spazmatism);
            }
            return true;
        }
        public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
        {
            Chains.ReportDroprates(ChainedRules, 1f, drops, ratesInfo);
        }
    }
}