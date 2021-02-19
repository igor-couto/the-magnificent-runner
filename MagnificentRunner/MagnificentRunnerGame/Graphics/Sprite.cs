using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunnerGame.Graphics
{
    public class Sprite
    {
        public Texture2D SpriteSheet { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color TintColor { get; set; } = Color.White;

        private Rectangle _rectangle;

        public Sprite(Texture2D spriteSheet, int x, int y, int width, int height, Color? tintColor = null)
        {
            SpriteSheet = spriteSheet;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            _rectangle = new Rectangle(x, y, width,height);

            if (tintColor != null)
                TintColor = tintColor.Value;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
            => spriteBatch.Draw(SpriteSheet, position, _rectangle, TintColor);
    }
}