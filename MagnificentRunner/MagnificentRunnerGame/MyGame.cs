using MagnificentRunner.MagnificentRunnerGame.Graphics;
using MagnificentRunner.MagnificentRunnerGame.Managers;
using MagnificentRunnerGame;
using MagnificentRunnerGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunner
{
    public class MyGame : Game
    {
        public const int ScreenSize = 3000;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly EntityManager _entityManager;
        private readonly GroundManager _groundManager;
        private const string SPRITESHEET = "Graphics/player";
        private Texture2D _spritesheetTexture;
        
        private readonly Player _player;
        private readonly Camera _camera;

        private SoundEffect _music;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.ApplyChanges();

            _camera = new Camera(_graphics.GraphicsDevice.Viewport);

            Window.AllowAltF4 = true;

            Content.RootDirectory = "Content/bin";
            IsMouseVisible = true;

            _player = new Player();
            _entityManager = new EntityManager();
            _groundManager = new GroundManager();

            _entityManager.AddEntity(_player);
        }

        protected override void Initialize() 
        {
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.IsFullScreen = false;
         
            _graphics.ApplyChanges();

            base.Initialize();
        } 
        
        protected override void LoadContent() 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var xablau = Content.Load<Texture2D>("Graphics/ground");
            _groundManager.InitializeResources(xablau);

            _spritesheetTexture = Content.Load<Texture2D>(SPRITESHEET);
            _player.InitializeResources(Content, _spritesheetTexture);

            _music = Content.Load<SoundEffect>("Sounds/music");
            _music.Play(volume: 0.5f, pitch: 0, pan: 0);
        }

        protected override void Update(GameTime gameTime)
        {
            //_camera.Update(_player.Position, gameTime, _graphics.PreferredBackBufferWidth , _graphics.PreferredBackBufferHeight);
            _camera.UpdateCamera(_graphics.GraphicsDevice.Viewport);

            UpdateFps(gameTime);

            InputManager.Instance.Update();
            _entityManager.UpdateEntities(gameTime);
            _groundManager.Update(gameTime);

            if (InputManager.Instance.IsExiting)
                Exit();

            base.Update(gameTime);
        }

        private void UpdateFps(GameTime gameTime)
        {
            var fps = (1 / gameTime.ElapsedGameTime.TotalSeconds);
            Window.Title = fps.ToString();
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(new Color(29, 101, 180));

            _spriteBatch.Begin(
                transformMatrix: _camera.Transform,
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.None,
                rasterizerState: RasterizerState.CullNone);

            _entityManager.DrawEntities(_spriteBatch, gameTime);

            _groundManager.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}