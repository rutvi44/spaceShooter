using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GGroup5
{
    public class StartScene : GameScene
    {
        private SoundEffect gunshotSound;
        private MenuComponent menuComponent;
        public MenuComponent MenuComponent { get => menuComponent; set => menuComponent = value; }

        private SpriteBatch spriteBatch;

        private Texture2D welcomeImageLeft;
        private Texture2D welcomeImageRight;
        private Texture2D backgroundImage;

        string[] menuItems = { "Start game", "Help", "High Score", "About", "Quit" };

        public StartScene(Game1 game, SoundEffect gunshotSound) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            this.backgroundImage = backgroundImage;

            this.gunshotSound = gunshotSound;

            // Load images
            welcomeImageLeft = g.Content.Load<Texture2D>("ship4");
            welcomeImageRight = g.Content.Load<Texture2D>("ship4");
            // Load fonts
            SpriteFont welcomeFont = g.Content.Load<SpriteFont>("welcome");
            SpriteFont regularFont = g.Content.Load<SpriteFont>("regularFont");
           SpriteFont highlightFont = g.Content.Load<SpriteFont>("highlightFont");

            // Initialize menu
            menuComponent = new MenuComponent(g, spriteBatch, regularFont, highlightFont, welcomeFont, menuItems);

            this.Components.Add(menuComponent);
        }
        public void ShowScene()
        {
            this.Enabled = true;
            this.Visible = true;


            // Ensure the MenuComponent is enabled and visible
            menuComponent.Enabled = true;
            menuComponent.Visible = true;

            // Other logic to reset or handle the StartScene's display
            // ...
        }

        public void DrawWelcomeMessage(SpriteBatch spriteBatch, SpriteFont welcomeFont, Texture2D imageLeft, Texture2D imageRight)
        {
            // Draw the left image before the welcome message
            Vector2 leftImagePosition = new Vector2(
                (GraphicsDevice.Viewport.Width - imageLeft.Width) / 8,
                (GraphicsDevice.Viewport.Height - imageLeft.Height) / 13);
            spriteBatch.Begin();
            spriteBatch.Draw(imageLeft, leftImagePosition, Color.White);
            spriteBatch.End();

            // Draw the right image after the welcome message
            Vector2 rightImagePosition = new Vector2(950, 45);
            spriteBatch.Begin();
            spriteBatch.Draw(imageRight, rightImagePosition, Color.White);
            spriteBatch.End();

            // Draw the welcome message on the screen
            string welcomeMessage = "Welcome to Space Shooter Game!";
            Vector2 welcomeSize = welcomeFont.MeasureString(welcomeMessage);
            Vector2 welcomePosition = new Vector2(
                (GraphicsDevice.Viewport.Width - welcomeSize.X) / 2,
                (GraphicsDevice.Viewport.Height - welcomeSize.Y) / 12);
            spriteBatch.Begin();
            spriteBatch.DrawString(welcomeFont, welcomeMessage, welcomePosition, Color.Orange);
            spriteBatch.End();
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw the welcome message with the left and right images
            DrawWelcomeMessage(spriteBatch, menuComponent.welcome, welcomeImageLeft, welcomeImageRight);

            base.Draw(gameTime);
        }

    }
}
