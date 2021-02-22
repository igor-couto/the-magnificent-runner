using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunner.MagnificentRunnerGame.Interfaces
{
    public interface IDrawable
    {
        Vector2 Position { get; set; }
        int DepthOrder { get; set; }
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
