using MagnificentRunner.MagnificentRunnerGame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MagnificentRunner.MagnificentRunnerGame.Managers
{
    class GroundManager
    {
        private List<GroundTile> _grounds;
        private List<GroundTile> _visibleGroundTiles;
        private const float POSITION_GROUND = 28;
        private float _speed = 10f;
        private int screenWidth = 800;

        public GroundManager() 
        {
            _grounds = new List<GroundTile>();
            _visibleGroundTiles = new List<GroundTile>();
        }

        public void InitializeResources(Texture2D spriteSheet) 
        {
            var defaultGroundTile = new GroundTile();
            defaultGroundTile.InitializeResources(spriteSheet);
            defaultGroundTile.Position = new Vector2(0, POSITION_GROUND);
            _grounds.Add(defaultGroundTile);

            var defaultGroundTile2 = (GroundTile) defaultGroundTile.Clone();
            defaultGroundTile2.Position = new Vector2( 128, POSITION_GROUND);

            _visibleGroundTiles.Add(defaultGroundTile);
            //_visibleGroundTiles.Add(defaultGroundTile2);
        }

        public void Update(GameTime gameTime)
        {
            var velocity = Vector2.Zero;
            velocity.X = -_speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            MoveTiles(velocity);



            if (_visibleGroundTiles[0].Position.X <= 0.0f && _visibleGroundTiles.Count == 1) 
            {


                var newTile = _grounds[new Random().Next(0, _grounds.Count)];
                newTile.Position = new Vector2(800, POSITION_GROUND);
                _visibleGroundTiles.Add(newTile);
            }
            

            if (_visibleGroundTiles[0].Position.X < -(screenWidth +180))
                _visibleGroundTiles.RemoveAt(0);

            //foreach (var groundTile in _visibleGroundTiles) 
            //{
            //    var velocity = Vector2.Zero;
            //    velocity.X = -_speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            //    groundTile.Position += velocity;

            //    if (groundTile.Position.X < 0.0f) 
            //        _visibleGroundTiles.Add(_grounds[new Random().Next(0, _grounds.Count)]);

            //    //if (groundTile.Position.X < (-screenWidth * 2))
            //    //    _visibleGroundTiles.RemoveAt(0);
            //}

            Console.WriteLine(_visibleGroundTiles[0].Position.X);
            Console.WriteLine(_visibleGroundTiles[1].Position.X);
        }

        private void MoveTiles(Vector2 velocity)
        {
            foreach(var visibleGroundTile in _visibleGroundTiles)
                visibleGroundTile.Position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var visibleGroundTile in _visibleGroundTiles)
                visibleGroundTile.Draw(spriteBatch, gameTime);
        }
    }
}
