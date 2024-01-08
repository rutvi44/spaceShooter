using GGroup5;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GGroup5
{
    public class Shoot : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private List<Projectile> projetliles;
        private float projectileSpeed = 5f;
        private TimeSpan projectileLifespan = TimeSpan.FromSeconds(3); 
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private SoundEffect gunshotSound;
        public List<Projectile> GetProjectiles()
        {
            return projetliles;
        }

        public Shoot(Game game, SpriteBatch spriteBatch, Texture2D texture, SoundEffect gunshotShound)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.projetliles = new List<Projectile>(); 
            this.gunshotSound = gunshotShound;
        }

        public void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile(this.texture, position);
            projetliles.Add(projectile);

            //projectilePosition = position;

            if (gunshotSound != null)
            {
                gunshotSound.Play();
            }
        }

        public void RemoveProjectile(Projectile projectile)
        {
            projectile.Position = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var projectile in projetliles)
            {

                projectile.Position = new Vector2(projectile.Position.X , projectile.Position.Y - projectileSpeed);
                //Debug.WriteLine($"Project line posrion : {pojectilePostion}");
                if (projectile.Position.Y < 0)
                {
                    RemoveProjectile(projectile);
                }

            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();


            foreach (var projectile in projetliles)
            {
                if (projectile.Position != Vector2.Zero)
                {
                    spriteBatch.Draw(texture, new Rectangle((int)projectile.Position.X, (int)projectile.Position.Y, 60, 60), Color.White);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }


    
}
