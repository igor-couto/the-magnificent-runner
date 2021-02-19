using MagnificentRunner.MagnificentRunnerGame.Definitions;
using MagnificentRunnerGame.Graphics;
using MagnificentRunnerGame.Interfaces;
using MagnificentRunnerGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MagnificentRunner.MagnificentRunnerGame.Graphics
{
    public class Player : IEntity
    {
        public bool IsAlive { get; set; }
        public int DepthOrder { get; set; } = 1;
        public Vector2 Position { get; set; }
        private float _speed { get; set; } = 140.0f;
        public Sprite Sprite { get; set; }
        public Sprite SpriteJumping { get; set; }
        public Sprite SpriteLanding { get; set; }
        private SpriteAnimation _runningAnimation {get; set;}
        private PlayerState _playerState { get; set; } = PlayerState.Running;

        public Player() => Position = new Vector2(0, 0);

        public void InitializeResources(Texture2D spriteSheet)
        {
            Sprite = new Sprite(spriteSheet, x: 0, y: 0, width: 8, height: 8);
            
            var SpriteRunning = new Sprite(spriteSheet, x: 8, y: 0, width: 8, height: 8);
            SpriteJumping = new Sprite(spriteSheet, x: 16, y: 0, width: 8, height: 8);
            SpriteLanding = new Sprite(spriteSheet, x: 32, y: 0, width: 8, height: 8);

            _runningAnimation = new SpriteAnimation();
            _runningAnimation.AddFrame(Sprite, 0);
            _runningAnimation.AddFrame(SpriteRunning, 0.2f);
            _runningAnimation.Duration = 0.4f;
            _runningAnimation.Play();
        }
        
        public void Update(GameTime gameTime)
        {
            if (_playerState is PlayerState.Running)
                _runningAnimation.Update(gameTime);

            var _velocity = Vector2.Zero;

            if (InputManager.Instance.KeyDown(Keys.Down))
                _velocity.Y = _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.Up))
                _velocity.Y = -_speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (InputManager.Instance.KeyDown(Keys.Right))
                _velocity.X = _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.Left))
                _velocity.X = -_speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            Position += _velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (_playerState) 
            {
                case PlayerState.Running:
                    _runningAnimation.Draw(spriteBatch, Position);
                    break;
                case PlayerState.Jumping:
                    SpriteJumping.Draw(spriteBatch, Position);
                    break;
                case PlayerState.Landing:
                    SpriteLanding.Draw(spriteBatch, Position);
                    break;
            }
        }
    }
}