using Microsoft.Xna.Framework;

namespace MagnificentRunner.MagnificentRunnerGame.Interfaces
{
    public interface IEntity : IDrawable
    {
        void Update(GameTime gameTime);
    }
}