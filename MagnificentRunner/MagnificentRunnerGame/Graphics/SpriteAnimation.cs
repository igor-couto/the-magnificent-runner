using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunnerGame.Graphics
{
    public class SpriteAnimation
    {
        private List<SpriteAnimationFrame> _frames { get; set; }

        public SpriteAnimationFrame this[int index] 
        {
            get { return GetFrame(index); }
        }

        public float PlaybackProgress { get; private set; }

        public bool IsPlaying { get; set; }

        public float Duration { get; set; }

        public bool ShouldLoop { get; set; } = true;

        public SpriteAnimationFrame CurrentFrame 
        {
            get 
            { 
                return _frames
                    .Where(frame => frame.TimeStamp <= PlaybackProgress)
                    .LastOrDefault(); 
            }
        }

        public SpriteAnimationFrame GetFrame(int index)
        {
            if (index < 0 || index >= _frames.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "");

            return _frames[index];
        }

        public void AddFrame(Sprite sprite, float timeStamp)
        {
            if (_frames is null) 
                _frames = new List<SpriteAnimationFrame>();
            
            _frames.Add(new SpriteAnimationFrame(sprite, timeStamp));
        }

        public void Update(GameTime gameTime) 
        {
            if (IsPlaying is false) 
                return;

            PlaybackProgress += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            if (PlaybackProgress > Duration) 
            {
                PlaybackProgress = 0;
                if (!ShouldLoop) 
                    IsPlaying = false;
            }
                
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) => CurrentFrame?.Sprite.Draw(spriteBatch, position);
        
        public void Play() => IsPlaying = true;

        public void Stop() 
        { 
            IsPlaying = false;
            PlaybackProgress = 0;
        }
    }
}