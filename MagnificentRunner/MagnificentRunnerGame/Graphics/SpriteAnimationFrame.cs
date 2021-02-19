using System;

namespace MagnificentRunnerGame.Graphics
{
    public class SpriteAnimationFrame
    {
        public float TimeStamp { get; }
        private Sprite _sprite;

        public SpriteAnimationFrame(Sprite sprite, float timeStamp)
        {
            Sprite = sprite;
            TimeStamp = timeStamp;
        }
        
        public Sprite Sprite { 
            get 
            {
                return _sprite;
            }
            set 
            {
                _sprite = value ?? throw new ArgumentNullException("value", "The sprite cannot be null");
            } 
        }
    }
}