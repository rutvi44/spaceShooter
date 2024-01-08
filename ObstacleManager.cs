using GGroup5;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGroup5
{
    public class ObstacleManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D obstacleTexture;
        private List<Obstacle> obstacles;
        public List<Obstacle> Obstacles { get { return obstacles; } } // Getter for obstacles
        private Random random;
        private int obstacleSpeed = 1; // Adjust the speed of obstacles
        private float obstacleTimer = 0f;
        private float obstacleDelay = 10f; // Delay in seconds between obstacles

        public ObstacleManager(Game game, SpriteBatch spriteBatch, Texture2D obstacleTexture)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.obstacleTexture = obstacleTexture;
            this.obstacles = new List<Obstacle>();
            this.random = new Random();
        }
        public void RemoveObstacle(Obstacle obstacle)
        {
            obstacles.Remove(obstacle);
        }
        public override void Update(GameTime gameTime)
        {
            obstacleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check the timer and create an obstacle after the delay
            if (obstacleTimer >= obstacleDelay)
            {
                obstacleTimer = 8f;

                // Generate new obstacle
                int obstacleX = random.Next(0, (int)Shared.stage.X - obstacleTexture.Width); // Random X position within the screen width
                Obstacle newObstacle = new Obstacle(obstacleTexture, new Vector2(obstacleX, -obstacleTexture.Height)); // Start above the screen

                // Add the new obstacle to the list
                obstacles.Add(newObstacle);

            }

            // Update existing obstacles
            foreach (var obstacle in obstacles.ToList())
            {
                obstacle.Position = new Vector2(obstacle.Position.X, obstacle.Position.Y + obstacleSpeed);

                // Check if the obstacle is completely off-screen, then remove it
                if (obstacle.Position.Y > Shared.stage.Y + obstacleTexture.Height)
                {
                    obstacles.Remove(obstacle);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (var obstacle in obstacles)
            {
                spriteBatch.Draw(obstacleTexture, obstacle.Position, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Obstacle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        // Add a Bounds property to represent the obstacle's bounding rectangle
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        public Obstacle(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
    }

}
