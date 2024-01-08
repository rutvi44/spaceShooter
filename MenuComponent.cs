using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace GGroup5
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public SpriteFont regularFont, highlightFont, welcome;
        private string[] menuItems;

        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color hilighghtColor = Color.Teal;
      

        private KeyboardState oldState;
        private MouseState oldMouseState;

        public MenuComponent(Game game,
          SpriteBatch spriteBatch,
          SpriteFont regularFont,
          SpriteFont highlightFont,
          SpriteFont welcome,
          string[] menuItems) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            this.welcome = welcome;
            this.menuItems = menuItems;
            // Calculate the center position for the menu
            float menuWidth = menuItems.Max(item => regularFont.MeasureString(item).X);
            float menuHeight = menuItems.Length * regularFont.LineSpacing;
            position = new Vector2(
                 (GraphicsDevice.Viewport.Width - menuWidth) / 2,
                 (GraphicsDevice.Viewport.Height - menuHeight) / 3);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            // Handle keyboard input
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Length)
                {
                    SelectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Length - 1;
                }
            }

            // Handle mouse input
            for (int i = 0; i < menuItems.Length; i++)
            {
                Rectangle menuItemRect = new Rectangle(
                    (int)position.X,
                    (int)position.Y + i * regularFont.LineSpacing,
                    (int)regularFont.MeasureString(menuItems[i]).X,
                    regularFont.LineSpacing);

                if (menuItemRect.Contains(mouseState.Position))
                {
                    SelectedIndex = i;
                }
            }

            oldState = ks;
            oldMouseState = mouseState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;

            spriteBatch.Begin();
            

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPos, hilighghtColor);
                    tempPos.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public string SelectedItem
        {
            get { return menuItems[SelectedIndex]; }
        }
    }
}
