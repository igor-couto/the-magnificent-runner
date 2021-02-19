using MagnificentRunner.MagnificentRunnerGame.Graphics;
using MagnificentRunnerGame;
using MagnificentRunnerGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MagnificentRunner
{
    public class MyGame : Game
    {
        public const int ScreenSize = 3000;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly EntityManager _entityManager;
        private const string ASSET_SPRITESHEET_URBAN = "Graphics/player";
        private Texture2D _spritesheetTexture;
        
        private readonly Player _player;
        private readonly Camera _camera;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                SynchronizeWithVerticalRetrace = true,
                PreferredBackBufferHeight = 64,
                PreferredBackBufferWidth = 128,
                IsFullScreen = false
            };

            _graphics.ApplyChanges();

            _camera = new Camera(_graphics.GraphicsDevice.Viewport);

            Window.AllowAltF4 = true;

            Content.RootDirectory = "Content/bin";
            IsMouseVisible = true;

            _player = new Player();
            _entityManager = new EntityManager();

            _entityManager.AddEntity(_player);
        }

        protected override void Initialize() => base.Initialize();
        
        protected override void LoadContent() 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spritesheetTexture = Content.Load<Texture2D>(ASSET_SPRITESHEET_URBAN);
            _player.InitializeResources(_spritesheetTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            //_camera.Update(_player.Position, gameTime, _graphics.PreferredBackBufferWidth , _graphics.PreferredBackBufferHeight);
            _camera.UpdateCamera(_graphics.GraphicsDevice.Viewport);

            UpdateFps(gameTime);

            InputManager.Instance.Update();
            _entityManager.UpdateEntities(gameTime);

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
            GraphicsDevice.Clear(new Color(128,176,200));

            _spriteBatch.Begin(
                transformMatrix: _camera.Transform,
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.None,
                rasterizerState: RasterizerState.CullNone);

            _entityManager.DrawEntities(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}