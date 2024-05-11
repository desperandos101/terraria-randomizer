using static System.Random;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using Terraria.GameContent.Achievements;

using LootClass;
using Terraria.GameContent.ItemDropRules;
using ItemSwapper;
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

namespace ItemSwapper
{
    public class SetManagement : ModSystem
	{
		public static LootSet mySet = new LootSet();
		public static void ResetSet() {

			// TODO: Normalize NPC Drops

			#region PreHardmode
			#region Chests
			/*Surface*/mySet.AddChestPool(0, new int[] {0, 12, ItemID.WoodenCrate, ItemID.WoodenCrateHard}, new int[] {280, 281, 284, 285, 953, 946, 3068, 3069, 3084, 4341, ItemID.BabyBirdStaff}, 0, 1);
			/*Underground*/mySet.AddChestPool(0, new int[] {1, 8, 32, 50, 51, 56}, new int[] {49, 50, 53, 54, 975, 930, 997, 906}, 0, 2);
			/*Ivy*/mySet.AddChestPool(0, new int[] {10, ItemID.JungleFishingCrate, ItemID.JungleFishingCrateHard}, new int[] {211, 212, 213, 964, 3017, 2292, 753}, 0, 1);
			/*Ice*/mySet.AddChestPool(0, new int[] {11, ItemID.FrozenCrate, ItemID.FrozenCrateHard}, new int[] {670, 724, 950, 1319, 987, 1579, 669}, 0, 1);
			/*Sky*/mySet.AddChestPool(0, new int[] {13, ItemID.FloatingIslandFishingCrate, ItemID.FloatingIslandFishingCrateHard}, new int[] {159, 65, 158, 2219}, 3);
			/*Web*/mySet.AddChestPool(0, new int[] {15}, new int[] {939}, 1);
			/*Water*/mySet.AddChestPool(0, new int[] {17, ItemID.OceanCrate, ItemID.OceanCrateHard}, new int[] {186, 187, 277, 4404, 863}, 0, 1);
			/*Desert*/mySet.AddChestPool(0, new int[] {62, 69, ItemID.OasisCrate, ItemID.OasisCrateHard}, new int[] {934, 857, 4061, 4062, 4263, 4262, 4056, 4055, 4276}, 0, 1);
			/*Dungeon*/mySet.AddChestPool(0, new int[] {2, ItemID.DungeonFishingCrate, ItemID.DungeonFishingCrateHard}, new int[] {ItemID.Handgun, ItemID.AquaScepter, ItemID.MagicMissile, ItemID.BlueMoon, ItemID.CobaltShield, ItemID.Muramasa, ItemID.Valor}, 0, 1);
			/*Hell*/mySet.AddChestPool(0, new int[] {4, ItemID.LavaCrate, ItemID.LavaCrateHard}, new int[] {ItemID.Sunfury, ItemID.FlowerofFire, ItemID.Flamelash, ItemID.DarkLance, ItemID.HellwingBow}, 0, 1);
			#endregion
			#region NPC Drops
			/*Ale Tosser*/mySet.AddRulePool(0, new int[] {NPCID.DD2Bartender}, new int[] {ItemID.AleThrowingGlove}, -1);
			/*Bat Bat*/mySet.AddRulePool(0, new int[] {49, 634, 51, 60, 150, 93, 137, 151, 121, 152, 158}, new int[] {5097});
			/*Bezoar*/mySet.AddRulePool(0, new int[] {42, 176, 141}, new int[] {887});
			/*Blood Pool*/mySet.AddRulePool(0, new int[] {586, 587}, new int[] {4381, 4325, 4273});
			/*Bone Sword + Helmets*/mySet.AddRulePool(0, new int[] {21}, new int[] {1166, 954, 955});
			/*Cobalt Armor*/mySet.AddRulePool(0, new int[] {42, 43}, new int[] {960});
			/*Combat Wrench*/mySet.AddRulePool(0, new int[] {NPCID.Merchant}, new int[] {ItemID.CombatWrench}, -1);
			/*Compass*/mySet.AddRulePool(0, new int[] {494, 496, 498, 58, 16, 185, 167, 197}, new int[] {393});
			/*Chain Knife*/mySet.AddRulePool(0, new int[] {49}, new int[] {1325});
			/*Demon Scythe*/mySet.AddRulePool(0, new int[] {62, 66}, new int[] {272});
			/*Depth Meter*/mySet.AddRulePool(0, new int[] {494, 496, 498, 49, 51, 150, 93, 634}, new int[] {18});
			/*Diving Helmet*/mySet.AddRulePool(0, new int[] {65}, new int[] {268});
			/*Exotic Scimitar*/mySet.AddRulePool(0, new int[] {207}, new int[] {3349}, -1);
			/*Giant Harpy Feather*/mySet.AddRulePool(0, new int[] {NPCID.Harpy}, new int[] {ItemID.GiantHarpyFeather});
			/*Gladius + Armor*/mySet.AddRulePool(0, new int[] {481}, new int[] {4463, 3187});
			/*Harpoon*/mySet.AddRulePool(0, new int[] {111, 26, 29, 27, 28}, new int[] {160});
			/*Jellyfish Necklace*/mySet.AddRulePool(0, new int[] {NPCID.BlueJellyfish, NPCID.PinkJellyfish, NPCID.GreenJellyfish}, new int[] {ItemID.JellyfishNecklace});
			/*Magma Stone*/mySet.AddRulePool(0, new int[] {NPCID.Hellbat}, new int[] {ItemID.MagmaStone});
			/*Mandible Blade*/mySet.AddRulePool(0, new int[] {580, 581}, new int[] {3772});
			/*Metal Detector*/mySet.AddRulePool(0, new int[] {195}, new int[] {3102});
			/*Mining Set*/mySet.AddRulePool(0, new int[] {44}, new int[] {410});
			/*Money Trough*/mySet.AddRulePool(0, new int[] {489, 490, 586, 587}, new int[] {3213});
			/*Nazar*/mySet.AddRulePool(0, new int[] {NPCID.CursedSkull, NPCID.GiantCursedSkull, NPCID.CursedHammer, NPCID.CrimsonAxe, NPCID.EnchantedSword}, new int[] {ItemID.Nazar});
			/*Night Vision Helmet*/mySet.AddRulePool(0, new int[] {482, 483}, new int[] {3109});
			/*Obsidian Rose*/mySet.AddRulePool(0, new int[] {24}, new int[] {1323});
			/*Paintball Gun*/mySet.AddRulePool(0, new int[] {227}, new int[] {3350}, -1);
			/*Rally*/mySet.AddRulePool(0, new int[] {494, 496, 498}, new int[] {3285});
			/*Snowball Launcher*/mySet.AddRulePool(0, new int[] {NPCID.SnowFlinx}, new int[] {ItemID.SnowballLauncher});
			/*Shackle, Zombie Arm*/mySet.AddRulePool(0, new int[] {3}, new int[] {216, 1304});
			/*Shadow Armor*/mySet.AddRulePool(0, new int[] {6}, new int[] {956});
			/*Shark Tooth Necklace*/mySet.AddRulePool(0, new int[] {489, 490}, new int[] {3212});
			/*Slime Staff*/mySet.AddRulePool(0, new int[] {-3, 1, -8, -7, -9, -6, 147, 537, -10, 184, 204, 16, -5, -4, 535, 302, 333, 334, 335, 336, 141, 121, 138, 658, 659, 660}, new int[] {1309});
			/*Stylish Scissors*/mySet.AddRulePool(0, new int[] {NPCID.Stylist}, new int[] {3352}, -1);
			/*Tally Counter*/mySet.AddRulePool(0, new int[] {NPCID.CursedSkull, NPCID.DarkCaster, NPCID.AngryBones}, new int[] {ItemID.TallyCounter});
			/*Tentacle Spike*/mySet.AddRulePool(0, new int[] {956, 7, NPCID.BloodCrawler, NPCID.Crimera, NPCID.FaceMonster}, new int[] {5094});
			/*Wizard Hat*/mySet.AddRulePool(0, new int[] {45}, new int[] {238});

			/*Globals*/mySet.AddGlobalItems(0, new int[] {ItemID.Cascade, ItemID.BloodyMachete, ItemID.BladedGlove});
			#endregion
			#region Shops
			//The pool with the negative int key is for hardmode.
			/*Merchant*/mySet.AddShopPool(0, 17, new int[] {1991, 88});
			/*Zoologist*/mySet.AddShopPool(0, 633, new int[] {4759, 4672, 4716, ItemID.DarkHorseSaddle, ItemID.PaintedHorseSaddle, ItemID.MajesticHorseSaddle, ItemID.DiggingMoleMinecart}, 5);
			mySet.AddShopPool(1, -633, new int[] {ItemID.JoustingLance});
			/*Golfer*/mySet.AddShopPool(0, NPCID.Golfer, new int[] {ItemID.GolfCart});
			/*Dryad*/mySet.AddShopPool(0, 20, new int[] {114});
			/*Arms Dealer*/mySet.AddShopPool(0, 19, new int[] {95, 98, ItemID.QuadBarrelShotgun});
			/*Goblin Tinkerer*/mySet.AddShopPool(0, NPCID.GoblinTinkerer, new int[] {128, 398, 486});
			/*Witch Doctor*/mySet.AddShopPool(0, 228, new int[] {ItemID.ImbuingStation, 986, ItemID.PygmyNecklace});
			mySet.AddShopPool(1, -228, new int[] {ItemID.LeafWings, ItemID.HerculesBeetle}, 1);
			/*Mechanic*/mySet.AddShopPool(0, NPCID.Mechanic, new int[] {ItemID.MechanicalLens, ItemID.MechanicsRod});
			/*Tavernkeep*/mySet.AddShopPool(0, NPCID.DD2Bartender, new int[] {ItemID.DD2FlameburstTowerT1Popper, ItemID.DD2BallistraTowerT1Popper, ItemID.DD2ExplosiveTrapT1Popper, ItemID.DD2LightningAuraT1Popper});
			mySet.AddShopPool(1, -NPCID.DD2Bartender, new int[] {ItemID.DD2FlameburstTowerT2Popper, ItemID.DD2BallistraTowerT2Popper, ItemID.DD2ExplosiveTrapT2Popper, ItemID.DD2LightningAuraT2Popper, ItemID.DD2FlameburstTowerT3Popper, ItemID.DD2BallistraTowerT3Popper, ItemID.DD2ExplosiveTrapT3Popper, ItemID.DD2LightningAuraT3Popper}, 4);
			
			/*Demolitionist*/mySet.AddShopPool(0, NPCID.Demolitionist, new int[] {}, 1);
			/*Painter*/mySet.AddShopPool(0, NPCID.Painter, new int[] {}, 1);
			/*Golfer*/mySet.AddShopPool(0, NPCID.Golfer, new int[] {}, 1);
			/*Stylist*/mySet.AddShopPool(0, NPCID.Stylist, new int[] {}, 1);
			/*Clothier*/mySet.AddShopPool(0, NPCID.Clothier, new int[] {}, 1);
			/*Party Girl*/mySet.AddShopPool(0, NPCID.PartyGirl, new int[] {}, 1);
			#endregion
			#region Fishing
			/*Any Accessories*/mySet.AddFishPool(0, new int[] {2423, 3225, 2420});
			/*Blood Moon*/mySet.AddFishPool(0, new int[] {4382});
			/*Rockfish*/mySet.AddFishPool(0, new int[] {2320});
			/*Demon Conch*/mySet.AddFishPool(0, new int[] {4819, ItemID.BottomlessLavaBucket, ItemID.LavaAbsorbantSponge});
			/*Ocean*/mySet.AddFishPool(0, new int[] {2332, 2341, 2342});
			#endregion
			#region Crates
			/*Wood Crate Base*/mySet.AddRulePool(0, new int[] {ItemID.WoodenCrate, ItemID.WoodenCrateHard, ItemID.IronCrate, ItemID.IronCrateHard}, new int[] {ItemID.SailfishBoots, ItemID.TsunamiInABottle}, 2);
			/*Hardmode Sundial*/mySet.AddRulePool(2, new int[] {ItemID.WoodenCrateHard, ItemID.IronCrateHard, ItemID.GoldenCrateHard}, new int[] {ItemID.Sundial}, 2);
			/*Iron Crate*/mySet.AddRulePool(0, new int[] {ItemID.IronCrate, ItemID.IronCrateHard}, new int[] {ItemID.GingerBeard, ItemID.TartarSauce, ItemID.FalconBlade}, 2);
			/*Golden Crate*/mySet.AddRulePool(0, new int[] {ItemID.GoldenCrate, ItemID.GoldenCrateHard}, new int[] {ItemID.HardySaddle, ItemID.EnchantedSword}, 2);
			#endregion
			#region Smashables
			/*Shadow Orb*/mySet.AddSmashPool(0, new int[] {ItemID.Musket, ItemID.ShadowOrb, ItemID.Vilethorn, ItemID.BallOHurt, ItemID.BandofStarpower}, 3);
			/*Water Bolt*/mySet.AddSmashPool(0, new int[] {ItemID.WaterBolt});
			/*Corrupt Crate*/mySet.AddBiomeCratePool(new int[] {ItemID.CorruptFishingCrate, ItemID.CorruptFishingCrateHard}, mySet.GetOrbPool(ItemID.Musket));
			/*Crimson Crate*/mySet.AddBiomeCratePool(new int[] {ItemID.CrimsonFishingCrate, ItemID.CrimsonFishingCrateHard}, mySet.GetOrbPool(ItemID.Musket)); //no real reason to use the musket specifically
			#endregion
			/*Angler*/mySet.AddQuestPool(0, new int[] {ItemID.FuzzyCarrot, ItemID.AnglerHat, ItemID.HoneyAbsorbantSponge, ItemID.BottomlessHoneyBucket, ItemID.GoldenFishingRod, ItemID.BottomlessBucket, ItemID.SuperAbsorbantSponge, ItemID.GoldenBugNet, ItemID.FishHook, ItemID.FishMinecart, ItemID.HighTestFishingLine, ItemID.AnglerEarring, ItemID.TackleBox, ItemID.FishermansGuide, ItemID.WeatherRadio, ItemID.Sextant, ItemID.FishingBobber});
			#region Bosses
			/*King Slime*/mySet.AddRulePool(0, new int[] {NPCID.KingSlime}, new int[] {ItemID.SlimySaddle, ItemID.NinjaHood, ItemID.SlimeHook}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.KingSlimeBossBag}, new int[] {ItemID.SlimySaddle, ItemID.NinjaHood, ItemID.SlimeHook, ItemID.RoyalGel}, 2);
			/*EoC*/mySet.AddRulePool(0, new int[] {NPCID.EyeofCthulhu}, new int[] {}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.EyeOfCthulhuBossBag}, new int[] {3097}, 2);
			/*EoW*/mySet.AddRulePool(0, new int[] {13, 14, 15}, new int[] {}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.EaterOfWorldsBossBag}, new int[] {ItemID.WormScarf}, 2);
			/*BoC*/mySet.AddRulePool(0, new int[] {NPCID.BrainofCthulhu}, new int[] {}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.BrainOfCthulhuBossBag}, new int[] {ItemID.BrainOfConfusion}, 2);
			/*Skeletron*/mySet.AddRulePool(0, new int[] {NPCID.SkeletronHead}, new int[] {ItemID.BookofSkulls, ItemID.SkeletronHand}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.SkeletronBossBag}, new int[] {ItemID.BookofSkulls, ItemID.SkeletronHand, ItemID.BoneGlove}, 2);
			/*Queen Bee*/mySet.AddRulePool(0, new int[] {NPCID.QueenBee}, new int[] {ItemID.BeeGun, ItemID.BeeKeeper, ItemID.BeesKnees, ItemID.HoneyComb, ItemID.HoneyedGoggles}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.QueenBeeBossBag}, new int[] {ItemID.BeeGun, ItemID.BeeKeeper, ItemID.BeesKnees, ItemID.HoneyComb, ItemID.HoneyedGoggles, ItemID.HiveBackpack}, 2);
			/*Deerclops*/mySet.AddRulePool(0, new int[] {NPCID.Deerclops}, new int[] {ItemID.ChesterPetItem, ItemID.PewMaticHorn, ItemID.WeatherPain, ItemID.HoundiusShootius, ItemID.LucyTheAxe}, 2);
			mySet.AddRulePool(0, new int[] {ItemID.DeerclopsBossBag}, new int[] {ItemID.ChesterPetItem, ItemID.PewMaticHorn, ItemID.WeatherPain, ItemID.HoundiusShootius, ItemID.LucyTheAxe, ItemID.BoneHelm}, 2);
			/*Dark Mage*/mySet.AddRulePool(0, new int[] {NPCID.DD2DarkMageT1, NPCID.DD2DarkMageT3, ItemID.BossBagDarkMage}, new int[] {ItemID.SquireShield, ItemID.ApprenticeScarf, ItemID.WarTable}, 1);
			#endregion
			#endregion
			#region Hardmode
			#region Chests
			/*Jungle Chest*/mySet.AddChestPool(1, new int[] {23}, new int[] {ItemID.PiranhaGun});
			/*Corruption Chest*/mySet.AddChestPool(1, new int[] {24}, new int[] {ItemID.ScourgeoftheCorruptor});
			/*Crimson Chest*/mySet.AddChestPool(1, new int[] {25}, new int[] {ItemID.VampireKnives});
			/*Hallowed Chest*/mySet.AddChestPool(1, new int[] {26}, new int[] {ItemID.RainbowGun});
			/*Ice Chest*/mySet.AddChestPool(1, new int[] {27}, new int[] {ItemID.StaffoftheFrostHydra});
			/*Desert Chest*/mySet.AddChestPool(1, new int[] {65}, new int[] {ItemID.StormTigerStaff});
			#endregion
			#region NPC Drops
			/*Adhesive Bandage*/mySet.AddRulePool(1, new int[] {NPCID.AnglerFish, NPCID.RustyArmoredBonesAxe, NPCID.Werewolf}, new int[] {ItemID.AdhesiveBandage});
			/*Ancient Horn*/mySet.AddRulePool(1, new int[] {NPCID.DesertBeast}, new int[] {ItemID.AncientHorn});
			/*Armor Polish*/mySet.AddRulePool(1, new int[] {NPCID.ArmoredSkeleton, NPCID.BlueArmoredBones}, new int[] {ItemID.ArmorPolish});
			/*Bananarang*/mySet.AddRulePool(1, new int[] {NPCID.Clown}, new int[] {ItemID.Bananarang});
			/*Beam Sword*/mySet.AddRulePool(1, new int[] {NPCID.ArmoredSkeleton}, new int[] {ItemID.BeamSword});
			/*Blessed Apple*/mySet.AddRulePool(1, new int[] {NPCID.Unicorn}, new int[] {ItemID.BlessedApple});
			/*Blindfold*/mySet.AddRulePool(1, new int[] {NPCID.CorruptSlime, NPCID.Crimslime, NPCID.BloodMummy, NPCID.DarkMummy, NPCID.Slimeling, NPCID.Slimer2}, new int[] {ItemID.Blindfold});
			/*Bone Lee*/mySet.AddRulePool(1, new int[] {NPCID.BoneLee}, new int[] {ItemID.BlackBelt, ItemID.Tabi});
			/*Butcher's Chainsaw*/mySet.AddRulePool(1, new int[] {NPCID.Butcher}, new int[] {ItemID.ButchersChainsaw});
			/*Butterfly Dust*/mySet.AddRulePool(1, new int[] {NPCID.Moth}, new int[] {ItemID.ButterflyDust});
			/*Classy Cane*/mySet.AddRulePool(1, new int[] {NPCID.TaxCollector}, new int[] {ItemID.TaxCollectorsStickOfDoom}, -1);
			/*Deadly Sphere Staff*/mySet.AddRulePool(1, new int[] {NPCID.DeadlySphere}, new int[] {ItemID.DeadlySphereStaff});
			/*Death Sickle*/mySet.AddRulePool(1, new int[] {NPCID.Reaper}, new int[] {ItemID.DeathSickle});
			/*Djinn's Curse*/mySet.AddRulePool(1, new int[] {NPCID.DesertDjinn}, new int[] {ItemID.DjinnsCurse});
			/*Eye Spring*/mySet.AddRulePool(1, new int[] {NPCID.Eyezor}, new int[] {ItemID.EyeSpring});
			/*Frost Staff*/mySet.AddRulePool(1, new int[] {NPCID.IceElemental, NPCID.IcyMerman}, new int[] {ItemID.FrostStaff});
			/*Frozen Turtle Shell*/mySet.AddRulePool(1, new int[] {NPCID.IceTortoise}, new int[] {ItemID.FrozenTurtleShell});
			/*Ice Feather*/mySet.AddRulePool(1, new int[] {NPCID.IceGolem}, new int[] {ItemID.IceFeather});
			/*Inferno Fork*/mySet.AddRulePool(1, new int[] {NPCID.DiabolistRed}, new int[] {ItemID.InfernoFork});
			/*Keybrand*/mySet.AddRulePool(1, new int[] {NPCID.BlueArmoredBones, NPCID.RustyArmoredBonesAxe, NPCID.HellArmoredBones}, new int[] {ItemID.Keybrand, ItemID.BoneFeather, ItemID.WispinaBottle, ItemID.MagnetSphere, ItemID.MaceWhip});
			/*Megaphone*/mySet.AddRulePool(1, new int[] {NPCID.GreenJellyfish, NPCID.DarkMummy, NPCID.BloodMummy, NPCID.Pixie}, new int[] {ItemID.Megaphone});
			/*Moon Charm*/mySet.AddRulePool(1, new int[] {NPCID.Werewolf}, new int[] {ItemID.MoonCharm});
			/*Moon Stone*/mySet.AddRulePool(1, new int[] {NPCID.Vampire}, new int[] {ItemID.MoonStone, ItemID.BrokenBatWing}, 1);
			/*Nail Gun*/mySet.AddRulePool(1, new int[] {NPCID.Nailhead}, new int[] {ItemID.NailGun});
			/*Neptune's Shell*/mySet.AddRulePool(1, new int[] {NPCID.CreatureFromTheDeep}, new int[] {ItemID.NeptunesShell});
			/*Nimbus Rod*/mySet.AddRulePool(1, new int[] {NPCID.AngryNimbus}, new int[] {ItemID.NimbusRod});
			/*Paladin Stuff*/mySet.AddRulePool(1, new int[] {NPCID.Paladin}, new int[] {ItemID.PaladinsHammer, ItemID.PaladinsShield});
			/*Pocket Mirror*/mySet.AddRulePool(1, new int[] {NPCID.Medusa}, new int[] {ItemID.PocketMirror});
			/*Poison Staff*/mySet.AddRulePool(1, new int[] {NPCID.BlackRecluse}, new int[] {ItemID.PoisonStaff});
			/*Psycho Knife*/mySet.AddRulePool(1, new int[] {NPCID.Psycho}, new int[] {ItemID.PsychoKnife});
			/*Resonance Scepter*/mySet.AddRulePool(1, new int[] {NPCID.Princess}, new int[] {ItemID.PrincessWeapon}, -1);
			/*Rifle Scope + Sniper Rifle*/mySet.AddRulePool(1, new int[] {NPCID.SkeletonSniper}, new int[] {ItemID.RifleScope, ItemID.SniperRifle});
			/*Rocket Launcher*/mySet.AddRulePool(1, new int[] {NPCID.SkeletonCommando}, new int[] {ItemID.RocketLauncher});
			/*Rod of Discord*/mySet.AddRulePool(1, new int[] {NPCID.ChaosElemental}, new int[] {ItemID.RodofDiscord});
			/*Shadow Jousting Lance*/mySet.AddRulePool(1, new int[] {NPCID.GiantCursedSkull}, new int[] {ItemID.ShadowJoustingLance});
			/*Shadowbeam Staff*/mySet.AddRulePool(1, new int[] {NPCID.Necromancer}, new int[] {ItemID.ShadowbeamStaff});
			/*Skeleton Archer*/mySet.AddRulePool(1, new int[] {NPCID.SkeletonArcher}, new int[] {ItemID.MagicQuiver, ItemID.Marrow}, 1);
			/*Spectre Staff*/mySet.AddRulePool(1, new int[] {NPCID.RaggedCaster}, new int[] {ItemID.SpectreStaff});
			/*Tactical Shotgun*/mySet.AddRulePool(1, new int[] {NPCID.TacticalSkeleton}, new int[] {ItemID.TacticalShotgun});
			/*Tattered Bee Wing*/mySet.AddRulePool(1, new int[] {NPCID.MossHornet}, new int[] {ItemID.TatteredBeeWing});
			/*Toxic Flask*/mySet.AddRulePool(1, new int[] {NPCID.DrManFly}, new int[] {ItemID.ToxicFlask});
			/*Trifold Map*/mySet.AddRulePool(1, new int[] {NPCID.Clown, NPCID.GiantBat, NPCID.LightMummy}, new int[] {ItemID.TrifoldMap});
			/*Unholy Trident*/mySet.AddRulePool(1, new int[] {NPCID.RedDevil}, new int[] {ItemID.UnholyTrident, ItemID.FireFeather});
			/*Uzi*/mySet.AddRulePool(1, new int[] {NPCID.AngryTrapper}, new int[] {ItemID.Uzi});
			/*Vitamins*/mySet.AddRulePool(1, new int[] {NPCID.Corruptor, NPCID.FloatyGross}, new int[] {ItemID.Vitamins});

			/*Globals*/mySet.AddGlobalItems(1, new int[] {ItemID.Amarok, ItemID.Yelets, ItemID.Kraken, ItemID.HelFire, ItemID.RedsWings, ItemID.DTownsWings, ItemID.WillsWings, ItemID.CrownosWings, ItemID.CenxsWings, 3228, ItemID.Yoraiz0rWings, ItemID.JimsWings, ItemID.SkiphsWings, ItemID.LokisWings, ItemID.ArkhalisWings, ItemID.LeinforsWings, ItemID.GhostarsWings, ItemID.SafemanWings, ItemID.FoodBarbarianWings, ItemID.GroxTheGreatWings});

			/*Mimic*/mySet.AddRulePool(1, new int[] {NPCID.Mimic}, new int[] {ItemID.DualHook, ItemID.MagicDagger, ItemID.PhilosophersStone, ItemID.TitanGlove, ItemID.StarCloak, ItemID.CrossNecklace}, 0, 1);
			/*Ice Mimic*/mySet.AddRulePool(1, new int[] {NPCID.IceMimic}, new int[] {ItemID.ToySled, ItemID.Frostbrand, ItemID.IceBow, ItemID.FlowerofFrost}, 0, 1);
			/*Hallowed Mimic*/mySet.AddRulePool(1, new int[] {NPCID.BigMimicHallow}, new int[] {ItemID.DaedalusStormbow, ItemID.FlyingKnife, ItemID.CrystalVileShard, ItemID.IlluminantHook}, 0, 1);
			/*Corrupt Mimic*/mySet.AddRulePool(1, new int[] {NPCID.BigMimicCorruption}, new int[] {ItemID.DartRifle, ItemID.ClingerStaff, ItemID.ChainGuillotines, ItemID.PutridScent, ItemID.WormHook}, 0, 1);
			/*Crimson Mimic*/mySet.AddRulePool(1, new int[] {NPCID.BigMimicCrimson}, new int[] {ItemID.SoulDrain, ItemID.DartPistol, ItemID.FetidBaghnakhs, ItemID.FleshKnuckles, ItemID.TendonHook}, 0, 1);

			/*Summoner*/mySet.AddRulePool(1, new int[] {NPCID.GoblinSummoner}, new int[] {ItemID.ShadowFlameBow, ItemID.ShadowFlameHexDoll, ItemID.ShadowFlameKnife}, 0, 1);

			/*Hemogoblin Shark*/mySet.AddRulePool(1, new int[] {NPCID.GoblinShark}, new int[] {4270}, 0, 1);
			/*Blood Eel*/mySet.AddRulePool(1, new int[] {NPCID.BloodEelHead}, new int[] {ItemID.DripplerFlail}, 0, 1);
			/*Both Bloodies*/mySet.AddRulePool(1, new int[] {NPCID.GoblinShark, NPCID.BloodEelHead}, new int[] {ItemID.BloodHamaxe});
			/*Dreadnautilus*/mySet.AddRulePool(1, new int[] {NPCID.BloodNautilus}, new int[] {ItemID.SanguineStaff}, 0, 1);

			/*Mothron*/mySet.AddRulePool(1, new int[] {NPCID.Mothron}, new int[] {ItemID.MothronWings}, 0, 1);
			
			/*Pirates*/mySet.AddRulePool(1, new int[] {NPCID.PirateCaptain, NPCID.PirateCorsair, NPCID.PirateCrossbower, NPCID.PirateDeadeye, NPCID.PirateDeckhand, NPCID.PirateShip}, new int[] {ItemID.CoinGun, ItemID.Cutlass, ItemID.DiscountCard, ItemID.GoldRing, ItemID.LuckyCoin, ItemID.PirateStaff}, 0, 1);
			/*Ship*/mySet.AddRulePool(1, new int[] {NPCID.PirateShip}, new int[] {ItemID.PirateMinecart});

			/*Martians*/mySet.AddRulePool(1, new int[] {NPCID.ScutlixRider, NPCID.GigaZapper, NPCID.MartianEngineer, NPCID.MartianOfficer, NPCID.RayGunner, NPCID.GrayGrunt, NPCID.BrainScrambler, NPCID.ScutlixRider}, new int[] {ItemID.LaserDrill, ItemID.ChargedBlasterCannon, ItemID.AntiGravityHook, ItemID.BrainScrambler}, 0, 1);
			

			/*Martian Saucer*/mySet.AddRulePool(1, new int[] {NPCID.MartianSaucer}, new int[] {ItemID.Xenopopper, ItemID.XenoStaff, ItemID.LaserMachinegun, ItemID.ElectrosphereLauncher, ItemID.InfluxWaver, ItemID.CosmicCarKey}, 0, 1);
			#endregion
			#region Shops
			//Hardmode items sold by pre-hardmode npcs are in the pre-hardmode section
			/*Wizard*/mySet.AddShopPool(1, NPCID.Wizard, new int[] {ItemID.CrystalBall, ItemID.IceRod});
			/*Truffle*/mySet.AddShopPool(1, NPCID.Truffle, new int[] {ItemID.MushroomSpear, ItemID.Hammush});
			/*Pirate*/mySet.AddShopPool(1, NPCID.Pirate, new int[] {ItemID.Cannon, ItemID.ParrotCracker, ItemID.BunnyCannon});
			/*Steampunker*/mySet.AddShopPool(1, NPCID.Steampunker, new int[] {ItemID.Clentaminator, ItemID.SteampunkWings, ItemID.StaticHook, ItemID.Jetpack}, 2);
			/*Cyborg*/mySet.AddShopPool(1, NPCID.Cyborg, new int[] {ItemID.ProximityMineLauncher});
			/*Princess*/mySet.AddShopPool(1, NPCID.Princess, new int[] {}, 1);
			#endregion
			#region Fishing
			/*Obsidian Fish*/mySet.AddFishPool(1, new int[] {ItemID.ObsidianSwordfish});
			/*Scaly Truffle*/mySet.AddFishPool(1, new int[] {ItemID.ScalyTruffle});
			/*Crystal Serpent*/mySet.AddFishPool(1, new int[] {ItemID.CrystalSerpent});
			/*Toxikarp*/mySet.AddFishPool(1, new int[] {ItemID.Toxikarp});
			/*Bladetongue*/mySet.AddFishPool(1, new int[] {ItemID.Bladetongue});
			#endregion
			/*Angler 2*/mySet.AddQuestPool(1, new int[] {ItemID.HotlineFishingHook, ItemID.FinWings});
			#region Bosses
			/*Wall of Flesh*/mySet.AddRulePool(1, new int[] {NPCID.WallofFlesh}, new int[] {ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.FireWhip, ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.WallOfFleshBossBag}, new int[] {ItemID.BreakerBlade, ItemID.ClockworkAssaultRifle, ItemID.LaserRifle, ItemID.FireWhip, ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem, ItemID.DemonHeart}, 0, 1);
			/*Queen Slime*/mySet.AddRulePool(1, new int[] {NPCID.QueenSlimeBoss}, new int[] {4982, ItemID.Smolstar, ItemID.QueenSlimeMountSaddle, ItemID.QueenSlimeHook}, 0, 1);
			mySet.AddRulePool(1, new int [] {ItemID.QueenSlimeBossBag}, new int[] {4982, ItemID.Smolstar, ItemID.QueenSlimeMountSaddle, ItemID.QueenSlimeHook, ItemID.VolatileGelatin}, 0, 1);
			/*Destroyer*/mySet.AddRulePool(1, new int [] {NPCID.TheDestroyer}, new int[] {}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.DestroyerBossBag}, new int[] {ItemID.MechanicalWagonPiece}, 0, 1);
			/*The Twins*/mySet.AddRulePool(1, new int[] {NPCID.Retinazer, NPCID.Spazmatism}, new int[] {}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.TwinsBossBag}, new int[] {ItemID.MechanicalWheelPiece}, 0, 1);
			/*Skeletron Prime*/mySet.AddRulePool(1, new int[] {NPCID.SkeletronPrime}, new int[] {}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.SkeletronPrimeBossBag}, new int[] {ItemID.MechanicalBatteryPiece}, 0, 1);
			/*Plantera*/mySet.AddRulePool(1, new int[] {NPCID.Plantera}, new int[] {ItemID.GrenadeLauncher, ItemID.VenusMagnum, ItemID.NettleBurst, ItemID.LeafBlower, ItemID.FlowerPow, ItemID.WaspGun, ItemID.Seedler, ItemID.PygmyStaff, ItemID.ThornHook}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.PlanteraBossBag}, new int[] {ItemID.GrenadeLauncher, ItemID.VenusMagnum, ItemID.NettleBurst, ItemID.LeafBlower, ItemID.FlowerPow, ItemID.WaspGun, ItemID.Seedler, ItemID.PygmyStaff, ItemID.ThornHook, ItemID.SporeSac}, 0, 1);
			/*Golem*/mySet.AddRulePool(1, new int[] {NPCID.Golem}, new int[] {ItemID.Picksaw, ItemID.Stynger, ItemID.PossessedHatchet, ItemID.SunStone, ItemID.EyeoftheGolem, ItemID.HeatRay, ItemID.StaffofEarth, ItemID.GolemFist}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.GolemBossBag}, new int[] {ItemID.Picksaw, ItemID.Stynger, ItemID.PossessedHatchet, ItemID.SunStone, ItemID.EyeoftheGolem, ItemID.HeatRay, ItemID.StaffofEarth, ItemID.GolemFist, ItemID.ShinyStone}, 0, 1);
			/*Mourning Wood*/mySet.AddRulePool(1, new int[] {NPCID.MourningWood, ItemID.CursedSapling}, new int[] {ItemID.StakeLauncher, ItemID.NecromanticScroll, ItemID.SpookyHook, ItemID.SpookyTwig, ItemID.CursedSapling}, 0, 1);
			mySet.AddRulePool(1, new int[] {NPCID.MourningWood, ItemID.SpookyTwig}, new int[] {ItemID.StakeLauncher, ItemID.NecromanticScroll, ItemID.SpookyHook, ItemID.SpookyTwig, ItemID.CursedSapling, ItemID.WitchBroom}, 0, 1);
			/*Pumpking*/mySet.AddRulePool(1, new int[] {NPCID.Pumpking}, new int[] {ItemID.TheHorsemansBlade, ItemID.RavenStaff, ItemID.BatScepter, ItemID.CandyCornRifle, ItemID.JackOLanternLauncher, ItemID.BlackFairyDust, ItemID.SpiderEgg, ItemID.ScytheWhip}, 0, 1);
			/*Everscream*/mySet.AddRulePool(1, new int[] {NPCID.Everscream}, new int[] {ItemID.ChristmasTreeSword, ItemID.Razorpine, ItemID.FestiveWings, ItemID.ChristmasHook}, 0, 1);
			/*Santank*/mySet.AddRulePool(1, new int[] {NPCID.SantaNK1}, new int[] {ItemID.ElfMelter, ItemID.ChainGun}, 0, 1);
			/*Ice Queen*/mySet.AddRulePool(1, new int[] {NPCID.IceQueen}, new int[] {ItemID.BlizzardStaff, ItemID.NorthPole, ItemID.SnowmanCannon, ItemID.BabyGrinchMischiefWhistle, ItemID.ReindeerBells}, 0, 1);
			/*Empress of Light*/mySet.AddRulePool(1, new int[] {NPCID.HallowBoss}, new int[] {4952, ItemID.PiercingStarlight, ItemID.RainbowWhip, 4953, ItemID.EmpressBlade, ItemID.RainbowWings, ItemID.SparkleGuitar}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.FairyQueenBossBag}, new int[] {4952, ItemID.PiercingStarlight, ItemID.RainbowWhip, 4953, ItemID.EmpressBlade, ItemID.RainbowWings, ItemID.SparkleGuitar, ItemID.EmpressFlightBooster}, 0, 1);
			/*Duke Fishron*/mySet.AddRulePool(1, new int[] {NPCID.DukeFishron}, new int[] {ItemID.BubbleGun, ItemID.Flairon, ItemID.RazorbladeTyphoon, ItemID.TempestStaff, ItemID.Tsunami, ItemID.FishronWings}, 0, 1);
			mySet.AddRulePool(1, new int[] {ItemID.FishronBossBag}, new int[] {ItemID.BubbleGun, ItemID.Flairon, ItemID.RazorbladeTyphoon, ItemID.TempestStaff, ItemID.Tsunami, ItemID.FishronWings, ItemID.ShrimpyTruffle}, 0, 1);
			/*Ogre*/mySet.AddRulePool(1, new int[] {NPCID.DD2OgreT2, NPCID.DD2OgreT3, ItemID.BossBagOgre}, new int[] {ItemID.HuntressBuckler, ItemID.MonkBelt, ItemID.DD2PhoenixBow, 3835, ItemID.DD2SquireDemonSword, 3836, ItemID.DD2PetGhost}, 0, 1);
			/*Betsy*/mySet.AddRulePool(1, new int[] {NPCID.DD2Betsy, ItemID.BossBagBetsy}, new int[] {ItemID.DD2BetsyBow, ItemID.BetsyWings, 3827, 3870, 3858});

			#endregion
			#endregion
			#region Config
			if (!ModContent.GetInstance<RandoConfig>().EnableFishing) {
				mySet.DisablePools(mySet.fishSet);
				mySet.DisablePools(mySet.questSet);
				mySet.DisablePools(mySet.dropRuleSet, s => s is DropRuleLootPool pool && ModifyCrates.mundaneCrateIDs.Contains(pool.registeredIDs[0]));
			}
			if (ModContent.GetInstance<RandoConfig>().EnableTownDrops) {
				mySet.GetRulePools(NPCID.DyeTrader)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.DD2Bartender)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.Stylist)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.Painter)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.Mechanic)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.TaxCollector)[0].randomSet = new int[1];
				mySet.GetRulePools(NPCID.Princess)[0].randomSet = new int[1];
			}
			#endregion
		}
        public override void PostWorldGen() 
		{
			mySet = new LootSet();
			ResetSet();

			if (WorldGen.crimson) {
				mySet.GetOrbPool(ItemID.Musket).initialSet = new int[] {ItemID.TheUndertaker, ItemID.CrimsonHeart, ItemID.PanicNecklace, ItemID.CrimsonRod, ItemID.TheRottedFork}; //THE ONLY REASON INITIALSET CANT BE READONLY FUCK MY LIIIIFE
				mySet.GetRulePools(13)[0].randoEnabled = false;
				mySet.GetRulePools(6)[0].randoEnabled = false;
				mySet.chestSet[24].randoEnabled = false;
				mySet.GetRulePools(NPCID.BigMimicCorruption)[0].randoEnabled = false;
			} else {
				mySet.GetRulePools(NPCID.BrainofCthulhu)[0].randoEnabled = false;
				mySet.chestSet[25].randoEnabled = false;
				mySet.GetRulePools(NPCID.BigMimicCrimson)[0].randoEnabled = false;
			}
			if (Main.GameMode == 1 || Main.GameMode == 2) {
				mySet.GetRulePools(NPCID.KingSlime)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.EyeofCthulhu)[0].randoEnabled = false;
				mySet.GetRulePools(13)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.BrainofCthulhu)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.SkeletronHead)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.QueenBee)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.Deerclops)[0].randoEnabled = false;

				mySet.GetRulePools(NPCID.WallofFlesh)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.QueenSlimeBoss)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.TheDestroyer)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.Retinazer)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.SkeletronPrime)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.Plantera)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.Golem)[0].randoEnabled = false;
				mySet.dropRuleSet.Remove(mySet.GetRulePools(ItemID.CursedSapling)[0]);
				mySet.GetRulePools(NPCID.HallowBoss)[0].randoEnabled = false;
				mySet.GetRulePools(NPCID.DukeFishron)[0].randoEnabled = false;
			} else if (Main.GameMode == 0) {
				mySet.GetRulePools(ItemID.KingSlimeBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.EyeOfCthulhuBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.EaterOfWorldsBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.BrainOfCthulhuBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.SkeletronBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.QueenBeeBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.DeerclopsBossBag)[0].randoEnabled = false;

				mySet.GetRulePools(ItemID.WallOfFleshBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.QueenSlimeBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.DestroyerBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.TwinsBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.SkeletronPrimeBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.PlanteraBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.GolemBossBag)[0].randoEnabled = false;
				mySet.dropRuleSet.Remove(mySet.GetRulePools(ItemID.SpookyTwig)[0]);
				mySet.GetRulePools(ItemID.FairyQueenBossBag)[0].randoEnabled = false;
				mySet.GetRulePools(ItemID.FishronBossBag)[0].randoEnabled = false;
			} else if (Main.GameMode == 3) {
				mySet.GetRulePools(ItemID.KingSlimeBossBag)[0].initialSet = new int[] {ItemID.RoyalGel};
				mySet.GetRulePools(ItemID.EyeOfCthulhuBossBag)[0].initialSet = new int[] {3097};
				mySet.GetRulePools(ItemID.EaterOfWorldsBossBag)[0].initialSet = new int[] {ItemID.WormScarf};
				mySet.GetRulePools(ItemID.BrainOfCthulhuBossBag)[0].initialSet = new int[] {ItemID.BrainOfConfusion};
				mySet.GetRulePools(ItemID.SkeletronBossBag)[0].initialSet = new int[] {ItemID.BoneGlove};
				mySet.GetRulePools(ItemID.QueenBeeBossBag)[0].initialSet = new int[] {ItemID.HiveBackpack};
				mySet.GetRulePools(ItemID.DeerclopsBossBag)[0].initialSet = new int[] {ItemID.BoneHelm};

				mySet.GetRulePools(ItemID.WallOfFleshBossBag)[0].initialSet = new int[] {ItemID.DemonHeart};
				mySet.GetRulePools(ItemID.QueenSlimeBossBag)[0].initialSet = new int[] {ItemID.VolatileGelatin};
				mySet.GetRulePools(ItemID.DestroyerBossBag)[0].initialSet = new int[] {ItemID.MechanicalWagonPiece};
				mySet.GetRulePools(ItemID.TwinsBossBag)[0].initialSet = new int[] {ItemID.MechanicalWheelPiece};
				mySet.GetRulePools(ItemID.SkeletronPrimeBossBag)[0].initialSet = new int[] {ItemID.MechanicalBatteryPiece};
				mySet.GetRulePools(ItemID.PlanteraBossBag)[0].initialSet = new int[] {ItemID.SporeSac};
				mySet.GetRulePools(ItemID.GolemBossBag)[0].initialSet = new int[] {ItemID.ShinyStone};
				mySet.dropRuleSet.Remove(mySet.GetRulePools(ItemID.CursedSapling)[0]);
				mySet.GetRulePools(ItemID.FairyQueenBossBag)[0].initialSet = new int[] {ItemID.EmpressFlightBooster};
				mySet.GetRulePools(ItemID.FishronBossBag)[0].initialSet = new int[] {ItemID.ShrimpyTruffle};

				mySet.GetRulePools(ItemID.KingSlimeBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.EyeOfCthulhuBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.EaterOfWorldsBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.BrainOfCthulhuBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.SkeletronBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.QueenBeeBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.DeerclopsBossBag)[0].randomSet = new int[1];

				mySet.GetRulePools(ItemID.WallOfFleshBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.QueenSlimeBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.DestroyerBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.TwinsBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.SkeletronPrimeBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.PlanteraBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.GolemBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.FairyQueenBossBag)[0].randomSet = new int[1];
				mySet.GetRulePools(ItemID.FishronBossBag)[0].randomSet = new int[1];

				mySet.GetRulePools(ItemID.WallOfFleshBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.QueenSlimeBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.DestroyerBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.TwinsBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.SkeletronPrimeBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.PlanteraBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.GolemBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.FairyQueenBossBag)[0].scoops = 0;
				mySet.GetRulePools(ItemID.FishronBossBag)[0].scoops = 0;
				
				mySet.GetRulePools(NPCID.KingSlime)[0].registeredIDs = new int[] {NPCID.KingSlime, ItemID.KingSlimeBossBag};
				mySet.GetRulePools(NPCID.EyeofCthulhu)[0].registeredIDs = new int[] {NPCID.EyeofCthulhu, ItemID.EyeOfCthulhuBossBag};
				mySet.GetRulePools(13)[0].registeredIDs = new int[] {13, ItemID.EaterOfWorldsBossBag};
				mySet.GetRulePools(NPCID.BrainofCthulhu)[0].registeredIDs = new int[] {NPCID.BrainofCthulhu, ItemID.BrainOfCthulhuBossBag};
				mySet.GetRulePools(NPCID.SkeletronHead)[0].registeredIDs = new int[] {NPCID.SkeletronHead, ItemID.SkeletronBossBag};
				mySet.GetRulePools(NPCID.QueenBee)[0].registeredIDs = new int[] {NPCID.QueenBee, ItemID.QueenBeeBossBag};
				mySet.GetRulePools(NPCID.Deerclops)[0].registeredIDs = new int[] {NPCID.Deerclops, ItemID.DeerclopsBossBag};

				mySet.GetRulePools(NPCID.WallofFlesh)[0].registeredIDs = new int[] {NPCID.WallofFlesh, ItemID.WallOfFleshBossBag};
				mySet.GetRulePools(NPCID.QueenSlimeBoss)[0].registeredIDs = new int[] {NPCID.QueenSlimeBoss, ItemID.QueenSlimeBossBag};
				mySet.GetRulePools(NPCID.TheDestroyer)[0].registeredIDs = new int[] {NPCID.TheDestroyer, ItemID.DestroyerBossBag};
				mySet.GetRulePools(NPCID.Retinazer)[0].registeredIDs = new int[] {NPCID.Retinazer, NPCID.Spazmatism, ItemID.TwinsBossBag};
				mySet.GetRulePools(NPCID.SkeletronPrime)[0].registeredIDs = new int[] {NPCID.SkeletronPrime, ItemID.SkeletronPrimeBossBag};
				mySet.GetRulePools(NPCID.Plantera)[0].registeredIDs = new int[] {NPCID.Plantera, ItemID.PlanteraBossBag};
				mySet.GetRulePools(NPCID.Golem)[0].registeredIDs = new int[] {NPCID.Golem, ItemID.GolemBossBag};
				mySet.GetRulePools(NPCID.HallowBoss)[0].registeredIDs = new int[] {NPCID.HallowBoss, ItemID.FairyQueenBossBag};
				mySet.GetRulePools(NPCID.DukeFishron)[0].registeredIDs = new int[] {NPCID.DukeFishron, ItemID.FishronBossBag};
			}

			mySet.Randomize();

			
        }
        public override void OnModLoad()
        {
            ResetSet(); //this is so drop rules are successfully added
			
        }
        public override void SaveWorldData(TagCompound tag)
        {
            tag[nameof(mySet)] = mySet;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            mySet = new LootSet();
			mySet = tag.Get<LootSet>(nameof(mySet));
        }
    }

	public class FishLoot : ModPlayer {
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
			LootSet mySet = SetManagement.mySet;
			LootPool pool = mySet.GetFishPool(itemDrop);
			if (pool != null) {
				int newItem = pool.GetNext();

				itemDrop = newItem;
				Item itemRef = new Item();
				itemRef.SetDefaults(newItem);

				sonar.Text = itemRef.Name;
				sonar.Color = ItemRarity.GetColor(itemRef.rare);
				sonar.DurationInFrames = 360;
				sonar.Velocity = new Vector2(0, -7f);
			}
			
        }
	}
}