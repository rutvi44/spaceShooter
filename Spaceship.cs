using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace GGroup5
{
    public class Spaceship : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle destinationRectangle;
        private Vector2 position;
        private Vector2 speed;
        //private Shoot shoot;
        private Texture2D projectileTexture;
        public Shoot ShootManager { get; private set; }

        public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);

        public Spaceship(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Texture2D projectileTex, SoundEffect gunshotSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.projectileTexture = projectileTex;
            position = new Vector2((Shared.stage.X - tex.Width) / 2,
                Shared.stage.Y - tex.Height);
            this.speed = new Vector2(4, 0);
            int screenWidth = game.GraphicsDevice.Viewport.Width;
            int screenHeight = game.GraphicsDevice.Viewport.Height;

            int spaceshipWidth = 100; // Adjust the spaceship width
            int spaceshipHeight = 100; // Adjust the spaceship height

            // Position the spaceship at the bottom center of the screen
            int xPos = (screenWidth - spaceshipWidth) / 2; // Centered horizontally
            int yPos = screenHeight - spaceshipHeight; // At the bottom

            destinationRectangle = new Rectangle(xPos, yPos, spaceshipWidth, spaceshipHeight);
            //shoot = new Shoot(game, spriteBatch, projectileTex);
            //game.Components.Add(shoot); // Add the Shoot component to the game components

            ShootManager = new Shoot(game, spriteBatch, projectileTex, gunshotSound);
            game.Components.Add(ShootManager);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, destinationRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed.X;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;
                if (position.X > Shared.stage.X - destinationRectangle.Width)
                {
                    position.X = Shared.stage.X - destinationRectangle.Width;
                }
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                // Call the AddProjectile method of the Shoot class
                ShootManager.AddProjectile(new Vector2(position.X + (tex.Width / 2), position.Y));
            }

            // Update the destinationRectangle using the position vector
            destinationRectangle.X = (int)position.X;

            base.Update(gameTime);
        }
    }
}
