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
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;
using Terraria.GameContent.Bestiary;
using Config;
using CrateDrop;
using Mono.CompilerServices.SymbolWriter;
using Terraria.WorldBuilding;
using Terraria.IO;
using System;
using System.Linq;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using MyExtensions;

namespace MajorItemRandomizer.RegionLocking {
    public static class RegionLocker {
        const int Forest = 1;
        const int Underground = 2;
        const int Snow = 3;
        const int Desert = 4;
        const int Jungle = 5;
        const int Dungeon = 6;
        const int WaterAndOcean = 7;
        const int Sky = 8;
        const int Hell = 9;
        const int Evil = 10;
        private static readonly (int, int[])[] BiomeChestSet = {
            (Forest, new int[] {0, 12}),
            (Underground, new int[] {1, 8, 15, 32, 50, 51, 56}),
            (Snow, new int[] {11}),
            (Desert, new int[] {62, 69}),
            (Jungle, new int[] {10}),
            (WaterAndOcean, new int[] {17}),
            (Sky, new int[] {13}),
        };
        private static readonly (int, int[])[] BiomeNPCSet = {//No npc set needed for the dungeon by technicality
            (Forest, new int[] {NPCID.GreenSlime, NPCID.BlueSlime, NPCID.PurpleSlime, NPCID.Pinky, NPCID.Zombie, NPCID.DemonEye, NPCID.Raven, NPCID.GoblinScout, NPCID.KingSlime, NPCID.PossessedArmor, NPCID.WanderingEye, NPCID.Wraith, NPCID.Werewolf, NPCID.HoppinJack}),
            (Underground, new int[] {NPCID.GiantWormHead, NPCID.RedSlime, NPCID.YellowSlime, NPCID.DiggerHead, NPCID.ToxicSludge,
            NPCID.BlackSlime, NPCID.MotherSlime, NPCID.BabySlime, NPCID.Skeleton, NPCID.CaveBat, NPCID.Salamander, NPCID.Crawdad, NPCID.GiantShelly, NPCID.UndeadMiner, NPCID.Tim, NPCID.Nymph, NPCID.CochinealBeetle,
            NPCID.Mimic, NPCID.ArmoredSkeleton, NPCID.GiantBat, NPCID.RockGolem, NPCID.SkeletonArcher, NPCID.RuneWizard,
            NPCID.GraniteGolem, NPCID.GraniteFlyer, NPCID.GreekSkeleton, NPCID.Medusa, NPCID.BlackRecluse,
            NPCID.AnomuraFungus, NPCID.FungiBulb, NPCID.MushiLadybug, NPCID.SporeBat, NPCID.SporeSkeleton, NPCID.ZombieMushroom,
            NPCID.FungoFish, NPCID.GiantFungiBulb}),
            (Snow, new int[] {NPCID.IceSlime, NPCID.ZombieEskimo, NPCID.CorruptPenguin, NPCID.CrimsonPenguin, NPCID.IceElemental, NPCID.Wolf, NPCID.IceGolem,
            NPCID.IceBat, NPCID.SnowFlinx, NPCID.SpikedIceSlime, NPCID.UndeadViking, NPCID.CyanBeetle, NPCID.ArmoredViking, NPCID.IceTortoise, NPCID.IceElemental, NPCID.IcyMerman, NPCID.IceMimic, NPCID.PigronCorruption, NPCID.PigronCrimson, NPCID.PigronHallow}),
            (Desert, new int[] {NPCID.Vulture, NPCID.Antlion, NPCID.Mummy, NPCID.LightMummy, NPCID.DarkMummy, NPCID.BloodMummy,
            NPCID.Tumbleweed, NPCID.SandElemental, NPCID.SandShark, NPCID.SandsharkCorrupt, NPCID.SandsharkCrimson, NPCID.SandsharkHallow,
            NPCID.WalkingAntlion, NPCID.LarvaeAntlion, NPCID.FlyingAntlion, NPCID.GiantWalkingAntlion, NPCID.GiantFlyingAntlion, NPCID.SandSlime, NPCID.TombCrawlerHead,
            NPCID.DesertBeast, NPCID.DesertScorpionWalk, NPCID.DesertLamiaLight, NPCID.DesertLamiaDark, NPCID.DuneSplicerHead, NPCID.DesertGhoul, NPCID.DesertGhoulCorruption, NPCID.DesertGhoulCrimson, NPCID.DesertGhoulHallow, NPCID.DesertDjinn}),
            (Jungle, new int[] {NPCID.JungleSlime, NPCID.JungleBat, NPCID.Snatcher, NPCID.DoctorBones, NPCID.Derpling, NPCID.GiantTortoise, NPCID.GiantFlyingFox, NPCID.Arapaima, NPCID.AngryTrapper,
            NPCID.Hornet, NPCID.ManEater, NPCID.SpikedJungleSlime, NPCID.LacBeetle, NPCID.JungleCreeper, NPCID.Moth, NPCID.MossHornet}),
            (WaterAndOcean, new int[] {NPCID.BlueJellyfish, NPCID.PinkJellyfish, NPCID.GreenJellyfish, NPCID.Piranha, NPCID.AnglerFish, NPCID.Crab, NPCID.Squid, NPCID.SeaSnail, NPCID.Shark}),
            (Sky, new int[] {NPCID.Harpy, NPCID.WyvernHead}),
            (Hell, new int[] {NPCID.Hellbat, NPCID.LavaSlime, NPCID.FireImp, NPCID.Demon, NPCID.VoodooDemon, NPCID.BoneSerpentHead, NPCID.Lavabat, NPCID.RedDevil}),
            (Evil, new int[] {NPCID.EaterofSouls, NPCID.CorruptGoldfish, NPCID.DevourerHead, NPCID.Corruptor, NPCID.CorruptSlime, NPCID.Slimeling, NPCID.Slimer, NPCID.Slimer2, NPCID.SeekerHead, NPCID.DarkMummy,
            NPCID.CursedHammer, NPCID.Clinger, NPCID.BigMimicCorruption, NPCID.DesertGhoulCorruption, NPCID.PigronCorruption,
            NPCID.BloodCrawler, NPCID.CrimsonGoldfish, NPCID.FaceMonster, NPCID.Crimera, NPCID.Herpling, NPCID.Crimslime, NPCID.BloodJelly, NPCID.BloodFeeder, NPCID.BloodMummy,
            NPCID.CrimsonAxe, NPCID.IchorSticker, NPCID.FloatyGross, NPCID.BigMimicCrimson, NPCID.DesertGhoulCrimson, NPCID.PigronCrimson})
        };
        private static Dictionary<int, bool> BiomeUnlocked = new Dictionary<int, bool> {
            {Forest, false},
            {Underground, false},
            {Snow, false},
            {Desert, false},
            {Jungle, false},
            {Dungeon, false},
            {WaterAndOcean, false},
            {Sky, true},
            {Hell, false},
            {Evil, false},
        };
        public static bool IsChestRegionLocked(this Tile chest) {
            int id = chest.IDChest();
            int biome = BiomeChestSet.UseAsDict(id);
            if (biome != 0) {
                return BiomeUnlocked[biome];
            }
            return true;
        }
        public static bool IsNPCRegionLocked(this DropAttemptInfo info) {
            int id = info.npc.IDNPC();
            int biome = BiomeNPCSet.UseAsDict(id);
            if (biome != 0) {
                return BiomeUnlocked[biome];
            }
            return true;
        }
    }
}