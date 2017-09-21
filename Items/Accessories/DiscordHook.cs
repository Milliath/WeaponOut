﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponOut.Items.Accessories
{
    public class DiscordHook : ModItem
    {
        public override bool Autoload(ref string name) { return ModConf.enableAccessories; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hook of Discord");
            Tooltip.SetDefault(
                "Teleports to the position of the mouse");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.IlluminantHook);
            item.shootSpeed += 0.5f;
            item.rare = 7;
            item.value += Item.sellPrice(0, 5, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RodofDiscord, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ItemID.Hook, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
            //Conversion from
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.RodofDiscord);
            recipe.AddRecipe();
        }
    }
}
