using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunnerGame.Interfaces
{
    public interface IEntity
    {
        int DepthOrder { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}