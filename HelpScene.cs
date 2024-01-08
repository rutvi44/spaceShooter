using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGroup5
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTex;
        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            helpTex = g.Content.Load<Texture2D>("help");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // Calculate the position to center the image on the screen
            Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);
            Vector2 imageCenter = new Vector2(helpTex.Width / 2f, helpTex.Height / 2f);
            Vector2 imagePosition = screenCenter - imageCenter;

            spriteBatch.Draw(helpTex, imagePosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
