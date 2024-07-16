using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Terraria.ObjectData;



namespace MajorItemRandomizer.RegionLocking
{
	public class ChestLock : GlobalTile
	{
        public override void RightClick(int i, int j, int type)
        {
            Tile tile = Main.tile[i, j];
            int left = i;
			int top = j;
			if (tile.TileFrameX % 36 != 0) {
				left--;
			}

			if (tile.TileFrameY != 0) {
				top--;
			}
            bool chestUnlocked = Main.tile[left, top].IsChestRegionLocked();
			if (!chestUnlocked) {
				Main.playerInventory = false;
				Main.NewText("CHEST IS LOCKED DUMMY");
            }
        }
    }
}