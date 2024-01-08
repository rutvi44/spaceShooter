using GGroup5;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GGroup5
{
    public class Projectile
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        public Projectile(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
        }
    }
}
