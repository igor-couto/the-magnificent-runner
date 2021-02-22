using MagnificentRunner.MagnificentRunnerGame.Definitions;
using MagnificentRunner.MagnificentRunnerGame.Interfaces;
using MagnificentRunnerGame.Graphics;
using MagnificentRunnerGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MagnificentRunner.MagnificentRunnerGame.Graphics
{
    public class Player : IEntity
    {
        public bool IsAlive { get; set; }
        public int DepthOrder { get; set; } = 1;
        public Vector2 Position { get; set; }
        private float _speed { get; set; } = 125f;
        public Sprite Sprite { get; set; }
        public Sprite SpriteJumping { get; set; }
        public Sprite SpriteLanding { get; set; }
        private SpriteAnimation _runningAnimation {get; set;}
        private PlayerState _state { get; set; } = PlayerState.Running;

        
        private const float GRAVITY = 1600f;
        private const float JUMP_INITAL_VELOCITY = -370f;
        private float _verticalVelocity = 0f;

        private SoundEffect[] _jumpingSounds;

        private SoundEffect _runningSound;
        private SoundEffect _fallingSound;
        private SoundEffect _dyingSound;
        private SoundEffect _landingSound;

        public Player() => Position = new Vector2(-60, 20);

        public void InitializeResources(ContentManager content, Texture2D spriteSheet)
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

            _jumpingSounds = new SoundEffect[] 
            {
                content.Load<SoundEffect>("Sounds/jumping1"),
                content.Load<SoundEffect>("Sounds/jumping2"),
                content.Load<SoundEffect>("Sounds/jumping3"),
                content.Load<SoundEffect>("Sounds/jumping4"),
                content.Load<SoundEffect>("Sounds/jumping5"),
            };

            //_runningSound = content.Load<SoundEffect>("");
            _dyingSound = content.Load<SoundEffect>("Sounds/dying");
            _fallingSound = content.Load<SoundEffect>("Sounds/falling");
            _landingSound = content.Load<SoundEffect>("Sounds/landing");
        }
        
        public void Update(GameTime gameTime)
        {
            if (_state is PlayerState.Landing)
                _state = PlayerState.Running;

            if (Position.Y >= 20 && _state is PlayerState.Jumping)
            {
                Position = new Vector2(Position.X, 20);
                _state = PlayerState.Landing;
                _verticalVelocity = 0;
                _landingSound.Play(volume: 0.1f, pitch: 0, pan: 0);
            }

            if (_state is PlayerState.Running)
                _runningAnimation.Update(gameTime);

            var velocity = Vector2.Zero;

            if (InputManager.Instance.KeyPressed(Keys.Up) && _state != PlayerState.Jumping)
                BeginJump();

            if (InputManager.Instance.KeyReleased(Keys.Up) && _state is PlayerState.Jumping)
                CancelJump();

            if (InputManager.Instance.KeyDown(Keys.Right))
                velocity.X = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (InputManager.Instance.KeyDown(Keys.Left))
                velocity.X = -_speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += velocity;

            if (_state is PlayerState.Jumping) 
            {
                Position = new Vector2(Position.X, Position.Y + _verticalVelocity * (float) gameTime.ElapsedGameTime.TotalSeconds);
                _verticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (_state) 
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

        private void BeginJump() 
        {
            _verticalVelocity = JUMP_INITAL_VELOCITY;
            _state = PlayerState.Jumping;
            _jumpingSounds[new Random().Next(0, 5)].Play();
        }

        private void CancelJump() 
        {
            _verticalVelocity = 0;
        }
    }
}