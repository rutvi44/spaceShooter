using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GGroup5
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<Developer> developers;

        public AboutScene(Game1 game, List<Developer> developers) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            this.font = g.Content.Load<SpriteFont>("about");

            this.developers = DevelopersManager.GetDevelopers();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            float xPosition = 400;

            // Draw information for each player using the sprite font
            float yPosition = 80; // You can adjust this value based on your preference

            foreach (var developer in developers)
            {
                spriteBatch.DrawString(font, $"Name: {developer.Name}", new Vector2(xPosition, yPosition), Microsoft.Xna.Framework.Color.Yellow);
                yPosition += 50; // Adjust the spacing
                spriteBatch.DrawString(font, $"StudentNumber: {developer.Number}", new Vector2(xPosition, yPosition), Microsoft.Xna.Framework.Color.Green);
                yPosition += 100; // Adjust the spacing
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
