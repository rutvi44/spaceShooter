using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;

namespace GGroup5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        Texture2D backgroundImage;
        public StartScene startScene;
        private ActionScene actionScene;
        public const int QUIT = 4;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        private MouseState oldMouseState;
        public SoundEffect gunshotSound;
        public SoundEffect blastSound;


        private HighSCorePage highScorePage;
        private SpriteFont spriteFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Set the preferred window size here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = 1200; // Set your desired width
            _graphics.PreferredBackBufferHeight = 700; // Set your desired height
            _graphics.ApplyChanges();

            Shared.stage = new System.Numerics.Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundImage = Content.Load<Texture2D>("background");

            gunshotSound = Content.Load<SoundEffect>("gunshot");
            blastSound = Content.Load<SoundEffect>("blast");

            startScene = new StartScene(this, gunshotSound);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this, DevelopersManager.GetDevelopers());
            this.Components.Add(aboutScene);


            spriteFont = Content.Load<SpriteFont>("about");

            highScorePage = new HighSCorePage(this, _spriteBatch, spriteFont);
            this.Components.Add(highScorePage);

            startScene.show();
            // TODO: use this.Content to load your game content here
        }
        public void HideActionScene()
        {
            actionScene.hide();

            GameTime simulatedGameTime = new GameTime(); // You might need to set properties of this instance as needed
            actionScene.Update(simulatedGameTime);
        }

        private void hideAllScenes()
        {
            foreach (var item in Components)
            {
                if (item is GameScene scene)
                {
                    scene.hide();
                }
            }
        }
  
        protected override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || ks.IsKeyDown(Keys.Escape))
                Exit();


            if (startScene.Enabled)
            {
                startScene.Update(gameTime);

                if ((startScene.MenuComponent.SelectedItem == "Start game" && ks.IsKeyDown(Keys.Enter)) || startScene.MenuComponent.SelectedItem == "Start game" &&
                    (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                {

                    startScene.hide();
                    actionScene.show();
                }
                else if ((startScene.MenuComponent.SelectedItem == "Help" && ks.IsKeyDown(Keys.Enter)) || startScene.MenuComponent.SelectedItem == "Help" &&
                    (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if ((startScene.MenuComponent.SelectedItem == "About" && ks.IsKeyDown(Keys.Enter)) || startScene.MenuComponent.SelectedItem == "About" &&
                    (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if ((startScene.MenuComponent.SelectedItem == "High Score" && ks.IsKeyDown(Keys.Enter)) || startScene.MenuComponent.SelectedItem == "High Score" &&
                    (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                {
                    startScene.hide();
                    highScorePage.show();
                }
                else if (startScene.MenuComponent.SelectedItem == "Quit" && ks.IsKeyDown(Keys.Enter) || startScene.MenuComponent.SelectedItem == "Quit" &&
                    (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released))
                {
                    Exit();
                }
            }

            if (actionScene.Enabled || helpScene.Enabled || aboutScene.Enabled || highScorePage.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }

                actionScene.Update(gameTime);

            }


            // Update old state for the next frame
            oldMouseState = ms;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            //Color darkPurple = new Color(47, 47, 47); // Adjust the RGB values as needed
            //GraphicsDevice.Clear(darkPurple);

            // Draw the background image
            _spriteBatch.Begin();
            _spriteBatch.Draw(Content.Load<Texture2D>("background"), new Vector2(0, 0), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}