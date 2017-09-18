﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponOut.Projectiles
{
    public class SpiritDragon : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Betsy's Rage");
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 104;
            projectile.height = 88;
            projectile.timeLeft = 30;

            projectile.alpha = 0;
            
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player == null || player.dead || !player.active) { projectile.timeLeft = 0; return; }
            projectile.timeLeft = player.itemAnimation - 2;
            if (projectile.timeLeft <= 0) return;

            player.heldProj = projectile.whoAmI; // draw over part of player

            projectile.Center = player.Center;
            projectile.velocity = default(Vector2);
            projectile.oldVelocity = player.velocity;
            projectile.spriteDirection = player.direction;
            projectile.localAI[0] = player.gravDir;

            if(projectile.ai[0] == 0)
            {
                projectile.frame = 0;
                if (projectile.localAI[1] == 0)
                { Main.PlaySound(SoundID.DD2_BetsyFireballShot, projectile.position); projectile.localAI[1]++; }
            }
            if (projectile.ai[0] == 1)
            {
                projectile.frame = 0;
                if (projectile.localAI[1] == 0)
                { Main.PlaySound(SoundID.DD2_BetsyFlameBreath, projectile.position); projectile.localAI[1]++; }
            }
            else if (projectile.ai[0] == 2)
            {
                projectile.frame = 1;
                if (projectile.localAI[1] == 0)
                { Main.PlaySound(SoundID.DD2_WyvernDiveDown, projectile.position); projectile.localAI[1]++; }
            }
            if (projectile.timeLeft < 10)
            {
                projectile.alpha += 24;
            }

            Lighting.AddLight(player.position + player.velocity, new Vector3(0.6f, 0.6f, 0.2f));
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D t = Main.projectileTexture[projectile.type];
            int frameHeight = t.Height / Main.projFrames[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection < 0) effects = SpriteEffects.FlipHorizontally;
            if (projectile.localAI[0] < 0) effects = effects | SpriteEffects.FlipVertically;
            Vector2 origin = new Vector2(t.Width / 2, frameHeight / 2);
            for (int i = 10; i >= 0; i--)
            {
                Vector2 drawPos = projectile.Center - Main.screenPosition - projectile.oldVelocity * i * 0.3f;
                float trailOpacity = projectile.Opacity - 0.2f * i;
                if (i != 0) trailOpacity /= 2f;
                if (trailOpacity > 0f)
                {
                    spriteBatch.Draw(t,
                        drawPos.ToPoint().ToVector2(),
                        new Rectangle(0, frameHeight * projectile.frame, t.Width, frameHeight),
                        new Color(1f, 1f, 1f, 0.5f) * trailOpacity,
                        projectile.rotation,
                        origin,
                        projectile.scale * (1f + 0.03f * i),
                        effects,
                        0);
                }
            }
            return false;
        }
    }
}