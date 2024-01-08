using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GGroup5
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private CollisionManager collisionManager; // Declare at the class level
        private Texture2D blastImageTexture;
        private SoundEffect gunshotSound;

        public ActionScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            this.gunshotSound = g.gunshotSound;
            Texture2D tex = g.Content.Load<Texture2D>("ship4");
            Texture2D projectileTexture = g.Content.Load<Texture2D>("gun1");
            Texture2D obstacleTexture = g.Content.Load<Texture2D>("obstacle2");
            blastImageTexture = g.Content.Load<Texture2D>("blastImage");
            SpriteFont font = g.Content.Load<SpriteFont>("about");
            Spaceship spaceship = new Spaceship(game, spriteBatch, tex, projectileTexture, gunshotSound);
            ObstacleManager obstacleManager = new ObstacleManager(game, spriteBatch, obstacleTexture);

            this.Components.Add(spaceship);
            this.Components.Add(obstacleManager);

            // Initialize the class-level collisionManager variable
            collisionManager = new CollisionManager(game, obstacleManager, spaceship.ShootManager, spaceship, spriteBatch, blastImageTexture, font, gunshotSound);
            this.Components.Add(collisionManager);
        }
    }
}
