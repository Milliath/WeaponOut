﻿using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using WeaponOut.Items.Weapons.UseStyles;
using System.Collections.Generic;

namespace WeaponOut.Items.Weapons
{
    public class FistsOfFury : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            return ModConf.enableFists;
        }

        public FistStyle fist;
        public FistStyle Fist
        {
            get
            {
                if (fist == null)
                {
                    fist = new FistStyle(item, 5);
                }
                return fist;
            }
        }
        public override void SetDefaults()
        {
            item.name = "Fists of Fury";
            item.toolTip = "Combos up to 5 times";
            item.toolTip2 = "Unleashes a fiery blast";
            item.useStyle = FistStyle.useStyle;
            item.useTurn = false;
            item.autoReuse = true;
            item.useAnimation = 30; //Half speed whilst combo-ing

            item.width = 28;
            item.height = 28;
            item.damage = 15;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item32;

            item.value = Item.sellPrice(0, 0, 24, 0);
            item.rare = 2;
            item.noUseGraphic = true;
            item.melee = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        public override bool AltFunctionUse(Player player)
        {
            player.GetModPlayer<PlayerFX>(mod).weaponDash = 1;
            return player.dashDelay == 0;
        }

        public override bool UseItemFrame(Player player)
        {
            FistStyle.UseItemFrame(player);
            Fist.UseItemFrameComboStop(player);
            return true;
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            noHitbox = FistStyle.UseItemHitbox(player, ref hitbox, 20);

            Rectangle graphic = FistStyle.UseItemGraphicbox(player, 12);
            Vector2 velo = FistStyle.GetFistVelocity(player) * -2f + player.velocity * 0.5f;
            int d = Dust.NewDust(graphic.TopLeft(), graphic.Width, graphic.Height, 174, velo.X, velo.Y);
            Main.dust[d].noGravity = true;
            for (int i = 0; i < 10; i++)
            {
                d = Dust.NewDust(graphic.TopLeft(), graphic.Width, graphic.Height, 174, velo.X * 1.2f, velo.Y * 1.2f);
                Main.dust[d].noGravity = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int combo = Fist.OnHitNPC(player, target, true);
            if (combo != -1)
            {
                if (combo % Fist.punchComboMax2 == 0)
                {
                    //set on fire
                    target.AddBuff(BuffID.OnFire, 300);
                }
            }
        }
    }
}
