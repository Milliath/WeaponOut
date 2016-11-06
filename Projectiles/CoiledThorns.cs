﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace WeaponOut.Projectiles
{
    public class CoiledThorns : ModProjectile
    {
        public const float whipLength = 38f;
        public const bool whipSoftSound = false;
        public const int handleHeight = 24;
        public const int chainHeight = 12;
        public const int partHeight = 16;
        public const int tipHeight = 12;
        public override void SetDefaults()
        {
            projectile.name = "Coiled Thorns";
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 1.1f;
            projectile.alpha = 255;
            projectile.aiStyle = 75;
            projectile.penetrate = -1;

            projectile.alpha = 0;
            projectile.hide = true;
            projectile.extraUpdates = 3;

            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.updatedNPCImmunity = true;
        }

        public override void AI()
        {
            Vector2 endPos = BaseWhip.WhipAI(projectile, whipLength);

            //Dust effect at the end
            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(endPos, projectile.width, projectile.height, 40, 0f, 0f, 0, Color.Transparent, 1.5f)];
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            BaseWhip.OnHitAny(projectile, target, crit, whipSoftSound);
            if (crit || Main.rand.Next(5) == 0) target.AddBuff(20, 240);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            BaseWhip.OnHitAny(projectile, crit, whipSoftSound);
            if (crit || Main.rand.Next(5) == 0) target.AddBuff(20, 240);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            BaseWhip.OnHitAny(projectile, crit, whipSoftSound);
            if (crit || Main.rand.Next(5) == 0) target.AddBuff(20, 240);
        }

        #region BaseWhip Stuff
        
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            BaseWhip.ModifyHitAny(projectile, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            BaseWhip.ModifyHitAny(projectile, ref damage, ref crit);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            BaseWhip.ModifyHitAny(projectile, ref damage, ref crit);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return BaseWhip.Colliding(projectile, targetHitbox);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return BaseWhip.PreDraw(projectile, handleHeight, chainHeight, partHeight, tipHeight, 10);
        }
        
        #endregion
    
    }
}