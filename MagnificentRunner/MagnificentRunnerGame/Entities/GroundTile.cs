using MagnificentRunner.MagnificentRunnerGame.Interfaces;
using MagnificentRunnerGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MagnificentRunner.MagnificentRunnerGame.Entities
{
    class GroundTile : IEntity, ICloneable
    {
        public Vector2 Position { get; set; }
        public int DepthOrder { get ; set ; }
        public Sprite Sprite { get; set; }

        public void InitializeResources(Texture2D spriteSheet)
            => Sprite = new Sprite(spriteSheet, x: 0, y: 0, width: 128, height: 16);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
            => Sprite.Draw(spriteBatch, Position);
        
        public void Update(GameTime gameTime)
        {
            
        }

        public object Clone()
        {
            return new GroundTile() 
            { 
                Position = this.Position,
                Sprite = this.Sprite
            };
        }
    }
}
