using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GGroup5
{
    public class BlastEffect : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D blastImageTexture;
        private Vector2 position;
        private Vector2 size;
        private TimeSpan duration;
        private TimeSpan elapsed;
        private SoundEffect blastSound;
        private bool soundPlayed;


        public BlastEffect(Game game, SpriteBatch spriteBatch, Texture2D blastImageTexture, SoundEffect blastSound, Vector2 position, Vector2 size, TimeSpan duration)
          : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.blastImageTexture = blastImageTexture;
            this.position = position;
            this.size = size;
            this.duration = duration;
            this.elapsed = TimeSpan.Zero;
            this.blastSound = blastSound;
            this.soundPlayed = false;
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime;

            if (elapsed >= duration)
            {
                this.Game.Components.Remove(this);
            }
            if (!soundPlayed && blastSound != null && elapsed >= duration - blastSound.Duration)
            {
                blastSound.Play();
                soundPlayed = true;
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blastImageTexture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
