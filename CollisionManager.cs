using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace GGroup5
{
    public class CollisionManager : DrawableGameComponent
    {
        private ObstacleManager obstacleManager;
        private Shoot shoot;
        private Spaceship spaceship;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch; 
        private Texture2D blastImageTexture;
        private float someThreshold = 100.0f;

        private int shotObstaclesCount = 0;
        private bool levelCompleted = false;
        private SpriteFont font;
        private SoundEffect blastSound;
        private int score = 0;
        private int missedObstaclePenalty = 25;
        public event EventHandler<GameCompletedEventArgs> GameCompleted;

        public CollisionManager(Game game, ObstacleManager obstacleManager, Shoot shoot, Spaceship spaceship, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Texture2D blastImageTexture, SpriteFont font, SoundEffect blastSound)
           : base(game)
        {
            this.obstacleManager = obstacleManager;
            this.shoot = shoot;
            this.spaceship = spaceship;
            this.spriteBatch = spriteBatch;
            this.blastImageTexture = blastImageTexture;
            this.font = font; // Set the font
            this.blastSound = blastSound;
        }

        public override void Update(GameTime gameTime)
        {
            // CheckObstacleSpaceshipCollision();
            //CheckProjectileObstacleCollision();
            // base.Update(gameTime);
            if (!levelCompleted)
            {
                CheckObstacleSpaceshipCollision();
                CheckProjectileObstacleCollision();

                // Check for missed obstacles and deduct points
                foreach (var obstacle in obstacleManager.Obstacles.ToList())
                {
                    if (obstacle.Position.Y > Shared.stage.Y)
                    {
                        // Obstacle is missed
                        obstacleManager.RemoveObstacle(obstacle);
                        score -= missedObstaclePenalty; // Deduct points for missed obstacle
                    }
                }
            }

            // Check for Enter key press to exit the game
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (levelCompleted)
                {
                    Environment.Exit(0); // Exit the game
                }
            }

            base.Update(gameTime);
        }
        private void OnGameCompleted()
        {
            GameCompleted?.Invoke(this, new GameCompletedEventArgs(score));
        }

        private void CheckObstacleSpaceshipCollision()
        {
            foreach (var obstacle in obstacleManager.Obstacles.ToList())
            {
                if (SpaceshipIntersectsObstacle(spaceship, obstacle))
                {
                    ((Game1)this.Game).startScene.ShowScene();
                    ((Game1)this.Game).HideActionScene();

                    break; 
                }
            }
        }
        

        private void CheckProjectileObstacleCollision()
        {
            foreach (var projectile in this.shoot.GetProjectiles())
            {
                foreach (var obstacle in obstacleManager.Obstacles.ToList())
                {
                    if (ProjectileIntersectsObstacle(projectile.Position, obstacle))
                    {                      
                        // Remove the obstacle after collision
                        obstacleManager.RemoveObstacle(obstacle);

                        // Trigger blast effect at the collision position
                        DisplayBlastEffect(obstacle.Position);

                        score += 50;

                        // Remove the projectile after collision
                        shoot.RemoveProjectile(projectile);
                        shotObstaclesCount++;
                        if (shotObstaclesCount == 10)
                        {
                            levelCompleted = true;
                        }
                        // Handle other collision logic here
                        break;
                    }
                }
            }
        }


        private bool ProjectileIntersectsObstacle(Vector2 projectilePosition, Obstacle obstacle)
        {
            float distance = Vector2.Distance(projectilePosition, obstacle.Position);
            return distance < someThreshold; // Adjust someThreshold as needed
        }

        private void DisplayBlastEffect(Vector2 collisionPosition)
        {
            TimeSpan duration = TimeSpan.FromSeconds(1); 
            //Vector2 blastSize = new Vector2(collisionPosition.X - 100, collisionPosition.Y);
            Vector2 blastSize = new Vector2(100, 100);
            BlastEffect blastEffect = new BlastEffect(this.Game, spriteBatch, blastImageTexture,blastSound ,collisionPosition, blastSize, duration);
            this.Game.Components.Add(blastEffect);
        }

        private bool SpaceshipIntersectsObstacle(Spaceship spaceship, Obstacle obstacle)
        {
            return spaceship.Bounds.Intersects(obstacle.Bounds);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (levelCompleted)
            {
                spriteBatch.Begin();
                string message = "Congratulations! Level 1 passed. Press Enter to start level 2.";
                Vector2 messageSize = font.MeasureString(message);
                Vector2 messagePosition = new Vector2((Shared.stage.X - messageSize.X) / 2, (Shared.stage.Y - messageSize.Y) / 2);
                spriteBatch.DrawString(font, message, messagePosition, Color.White);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            string scoreText = "Score: " + score;
            Vector2 scorePosition = new Vector2(10, 10);
            spriteBatch.DrawString(font, scoreText, scorePosition, Color.IndianRed);
            spriteBatch.End();
        }

    }
}
